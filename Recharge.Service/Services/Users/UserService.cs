using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Users;
using Recharge.Application.Interfaces.Users;
using Recharge.Application.Validator.User;
using Recharge.Domain.Models.Users;
using Recharge.Domain.Repositories.Users;
using Recharge.Infra.Data.Authentication;

namespace Recharge.Application.Services.Users;
public class UserService : IUserService {

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserValidator _userValidator;
    private readonly TokenService _tokenService;


    public UserService(IUserRepository userRepository, IMapper mapper, UserValidator userValidator, TokenService tokenService) {
        _userRepository = userRepository;
        _mapper = mapper;
        _userValidator = userValidator;
        _tokenService = tokenService;
    }

    public async Task<ResultService<UserResponseDTO>> RegisterUser(RegisterUserDTO userDTO) {
        try {

            if (!Validator.CustomUserValidator.SecurityLevel(userDTO.HashPassword)) {
                return ResultService.Fail<UserResponseDTO>("A senha não atende aos requisitos de segurança.");
            }

            userDTO.HashPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.HashPassword);

            var user = _mapper.Map<User>(userDTO);

            ValidationResult validationResult = await _userValidator.ValidateAsync(user);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<UserResponseDTO>("Erro ao registrar usuário.", validationResult);
            }

            var existingUser = await _userRepository.GetUserByEmail(user.Email);

            if (existingUser != null) {
                return ResultService.Fail<UserResponseDTO>("O e-mail já está em uso.");
            }

            var registeredUser = await _userRepository.RegisterUser(user);

            var userResponseDTO = _mapper.Map<UserResponseDTO>(registeredUser);

            return ResultService.Ok(userResponseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserResponseDTO>($"Erro ao registrar usuário: {ex.Message}");
        }
    }


    public async Task<ResultService<UserResponseDTO>> GetUserById(Guid id) {
        try {
            var user = await _userRepository.GetUserById(id);

            if (user == null) {
                return ResultService.Fail<UserResponseDTO>("Usuário não encontrado.");
            }

            var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return ResultService.Ok(userResponseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserResponseDTO>($"Erro ao obter usuário por ID: {ex.Message}");
        }
    }

    public async Task<ResultService<UserResponseDTO>> GetUserByEmail(string email) {
        try {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null) {
                return ResultService.Fail<UserResponseDTO>("Usuário não encontrado.");
            }

            var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return ResultService.Ok(userResponseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserResponseDTO>($"Erro ao obter usuário por e-mail: {ex.Message}");
        }
    }

    public async Task<ResultService<UserResponseDTO>> GetUserByDocument(string cpf) {
        try {
            var user = await _userRepository.GetUserByDocument(cpf);

            if (user == null) {
                return ResultService.Fail<UserResponseDTO>("Usuário não encontrado.");
            }

            var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            return ResultService.Ok(userResponseDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserResponseDTO>($"Erro ao obter usuário por CPF: {ex.Message}");
        }
    }

    public async Task<ResultService<ICollection<UserResponseDTO>>> GetAllUsers() {
        try {
            var users = await _userRepository.GetAllUsers();
            var userResponseDTOs = _mapper.Map<ICollection<UserResponseDTO>>(users);

            return ResultService.Ok(userResponseDTOs);
        } catch (Exception ex) {
            return ResultService.Fail<ICollection<UserResponseDTO>>($"Erro ao obter todos os usuários: {ex.Message}");
        }
    }

    public async Task<ResultService<UserUpdateDTO>> CompleteRegister(Guid id, UserUpdateDTO userDTO) {
        try {
            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null) {
                return ResultService.Fail<UserUpdateDTO>("Usuário não encontrado.");
            }

            var updatedUser = _mapper.Map(userDTO, existingUser);

            var validator = new UserValidator();
            ValidationResult validationResult = await validator.ValidateAsync(updatedUser);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<UserUpdateDTO>("Erro ao completar registro do usuário.", validationResult);
            }

            var completedUser = await _userRepository.CompleteRegister(updatedUser);
            var completedUserDTO = _mapper.Map<UserUpdateDTO>(completedUser);

            return ResultService.Ok(completedUserDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserUpdateDTO>($"Erro ao completar registro do usuário: {ex.Message}");
        }
    }

    public async Task<ResultService<UserUpdateDTO>> UpdateUser(Guid id, UserUpdateDTO userDTO) {
        try {
            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null) {
                return ResultService.Fail<UserUpdateDTO>("Usuário não encontrado.");
            }

            var updatedUser = _mapper.Map(userDTO, existingUser);

            var validator = new UserValidator();
            ValidationResult validationResult = await validator.ValidateAsync(updatedUser);

            if (!validationResult.IsValid) {
                return ResultService.RequestError<UserUpdateDTO>("Erro ao atualizar registro do usuário.", validationResult);
            }

            var completedUser = await _userRepository.CompleteRegister(updatedUser);
            var completedUserDTO = _mapper.Map<UserUpdateDTO>(completedUser);

            return ResultService.Ok(completedUserDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserUpdateDTO>($"Erro ao atualizar registro do usuário: {ex.Message}");
        }
    }

    public async Task<ResultService<UserResponseDTO>> DeleteUser(Guid id, UserDTO userDTO) {
        try {
            var existingUser = await _userRepository.GetUserById(id);

            if (existingUser == null) {
                return ResultService.Fail<UserResponseDTO>("Usuário não encontrado.");
            }

            var deletedUser = await _userRepository.DeleteUser(existingUser);
            var deletedUserDTO = _mapper.Map<UserResponseDTO>(deletedUser);

            return ResultService.Ok(deletedUserDTO);
        } catch (Exception ex) {
            return ResultService.Fail<UserResponseDTO>($"Erro ao excluir usuário: {ex.Message}");
        }
    }

    public async Task<string> LogIn(LoginDTO loginDTO) {
        try {
            if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.HashPassword))
                return ("E-mail e senha são obrigatórios para o login.");

            var existingUser = await _userRepository.GetUserByEmail(loginDTO.Email);

            if (existingUser == null) {
                return ("E-mail não cadastrado.");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDTO.HashPassword, existingUser.HashPassword);

            if (!isPasswordCorrect) {
                return ("Senha incorreta.");
            }

            await _userRepository.LogIn(existingUser.Id, true);

            var token = _tokenService.GenerateToken(existingUser);

            return (token);
        } catch (Exception ex) {
            return ($"Erro ao fazer login: {ex.Message}");
        }
    }
}