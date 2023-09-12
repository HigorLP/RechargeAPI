using Recharge.Domain.Models.Users;

namespace Recharge.Domain.Repositories.Users;
public interface IAddressRepository {
    Task<Address> CreateAddress(Address address);
    Task<Address> GetAddressById(Guid id);
    Task<ICollection<Address>> GetAddress();
    Task<Address> UpdateAddress(Address address);
    Task DeleteAddress(Address address);
}