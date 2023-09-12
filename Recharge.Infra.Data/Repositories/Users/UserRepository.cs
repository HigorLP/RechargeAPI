using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Users;
using Recharge.Domain.Repositories.Users;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Users;
public class UserRepository : IUserRepository {

    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<User> CompleteRegister(User user) {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteUser(User user) {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<ICollection<User>> GetAllUsers() {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByDocument(string cpf) {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Cpf == cpf);
    }

    public async Task<User> GetUserByEmail(string email) {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetUserById(Guid id) {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> GetUserDetail(Guid userId) {
        return await _dbContext.Users
            .Include(u => u.Addresses)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }


    public async Task<User> LogIn(Guid userId, bool status) {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user != null) {
            user.IsLogged = status;
            await _dbContext.SaveChangesAsync();
        }

        return user;
    }

    public async Task<User> RegisterUser(User user) {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user) {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}