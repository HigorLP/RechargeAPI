using Recharge.Application.DTOs.Users;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Users;
public interface IAddressService {
    Task<ResultService<AddressDTO>> CreateAddress(AddressDTO addressDTO);
    Task<ResultService<AddressDTO>> GetAddressById(Guid id);
    Task<ResultService<ICollection<AddressDTO>>> GetAddress(AddressDTO addressDTO);
    Task<ResultService<AddressDTO>> UpdateAddress(AddressDTO addressDTO);
    Task<ResultService> DeleteAddress(Guid id);
}