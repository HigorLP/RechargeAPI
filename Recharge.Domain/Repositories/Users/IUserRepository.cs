using Recharge.Domain.Models.Users;

namespace Recharge.Domain.Repositories.Users;
public interface IUserRepository {

    Task<User> RegisterUser(User user);
    Task<User> GetUserById(Guid id);
    Task<User> GetUserDetail(Guid userId);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByDocument(string cpf);
    Task<ICollection<User>> GetAllUsers();
    Task<User> CompleteRegister(User user);
    Task<User> UpdateUser(User user);
    Task<User> DeleteUser(User user);
    Task<User> LogIn(Guid userId, bool status);
}