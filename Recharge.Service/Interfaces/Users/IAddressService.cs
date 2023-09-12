using Recharge.Application.DTOs.Users;

namespace Recharge.Application.Interfaces.Users;
public interface IAddressService {
    Task<object> CreateAddress(AddressDTO addressDTO);
    Task<object> GetAddressById(Guid id);
    Task<ICollection<object>> GetAddress();
    Task<object> UpdateAddress(AddressDTO addressDTO);
    Task<object> DeleteAddress(Guid id);
}