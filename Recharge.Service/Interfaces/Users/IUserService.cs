using Recharge.Application.DTOs.Users;

namespace Recharge.Application.Interfaces.Users;
public interface IUserService {

    Task<object> RegisterUser(RegisterUserDTO user);
    Task<object> GetUserById(Guid id);
    Task<object> GetUserDetail(Guid id);
    Task<object> GetUserByEmail(string email);
    Task<object> GetUserByDocument(string cpf);
    Task<ICollection<object>> GetAllUsers();
    Task<object> CompleteRegister(Guid id, UserUpdateDTO userDTO);
    Task<object> UpdateUser(Guid id, UserUpdateDTO user);
    Task<object> DeleteUser(Guid id, UserDTO user);
    Task<string> LogIn(LoginDTO loginDTO);
}