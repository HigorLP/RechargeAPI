using Recharge.Application.DTOs.Users;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Users;
public interface IUserService {

    Task<ResultService<UserResponseDTO>> RegisterUser(RegisterUserDTO user);
    Task<ResultService<UserResponseDTO>> GetUserById(Guid id);
    Task<ResultService<UserResponseDTO>> GetUserByEmail(string email);
    Task<ResultService<UserResponseDTO>> GetUserByDocument(string cpf);
    Task<ResultService<ICollection<UserResponseDTO>>> GetAllUsers();
    Task<ResultService<UserUpdateDTO>> CompleteRegister(Guid id, UserUpdateDTO userDTO);
    Task<ResultService<UserUpdateDTO>> UpdateUser(Guid id, UserUpdateDTO user);
    Task<ResultService<UserResponseDTO>> DeleteUser(Guid id, UserDTO user);
    Task<string> LogIn(LoginDTO loginDTO);
}