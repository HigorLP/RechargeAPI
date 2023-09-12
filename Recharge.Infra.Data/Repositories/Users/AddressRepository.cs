using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Users;
using Recharge.Domain.Repositories.Users;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Users;
public class AddressRepository : IAddressRepository {

    private readonly ApplicationDbContext _dbContext;

    public AddressRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Address> CreateAddress(Address address) {
        _dbContext.Add(address);
        await _dbContext.SaveChangesAsync();
        return address;
    }

    public async Task<Address> GetAddressById(Guid id) {
        return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Address>> GetAddress() {
        return await _dbContext.Addresses.ToListAsync();
    }

    public async Task<Address> UpdateAddress(Address address) {
        _dbContext.Update(address);
        await _dbContext.SaveChangesAsync();
        return address;
    }

    public async Task DeleteAddress(Address address) {
        _dbContext.Remove(address);
        await _dbContext.SaveChangesAsync();
    }
}