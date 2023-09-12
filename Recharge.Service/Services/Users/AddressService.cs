using AutoMapper;
using Microsoft.AspNetCore.Http;
using Recharge.Application.DTOs.Users;
using Recharge.Application.Interfaces.Users;
using Recharge.Domain.Models.Users;
using Recharge.Domain.Repositories.Users;

namespace Recharge.Application.Services.Users {
    public class AddressService : IAddressService {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> CreateAddress(AddressDTO addressDTO) {
            try {
                var address = _mapper.Map<Address>(addressDTO);

                var createdAddress = await _addressRepository.CreateAddress(address);
                var createdAddressDTO = _mapper.Map<AddressDTO>(createdAddress);

                return (createdAddressDTO);
            } catch (Exception ex) {
                return ResultService.Fail<AddressDTO>($"Erro ao criar endereço: {ex.Message}");
            }
        }

        public async Task<object> GetAddressById(Guid id) {
            try {
                var address = await _addressRepository.GetAddressById(id);

                if (address == null) {
                    return ResultService.Fail<AddressDTO>("Endereço não encontrado.");
                }

                var addressDTO = _mapper.Map<AddressDTO>(address);
                return (addressDTO);
            } catch (Exception ex) {
                return ResultService.Fail<AddressDTO>($"Erro ao obter endereço por ID: {ex.Message}");
            }
        }

        public async Task<ICollection<object>> GetAddress() {
            try {

                var addresses = await _addressRepository.GetAddress();
                var addressDTOs = _mapper.Map<ICollection<AddressDTO>>(addresses);

                return new List<object>(addressDTOs);
            } catch (Exception ex) {
                return new List<object> { ($"Erro ao obter endereços: {ex.Message}") };
            }
        }

        public async Task<object> UpdateAddress(AddressDTO addressDTO) {
            try {
                var existingAddress = await _addressRepository.GetAddressById(addressDTO.Id.Value);

                if (existingAddress == null) {
                    return ResultService.Fail<AddressDTO>("Endereço não encontrado.");
                }

                var updatedAddress = _mapper.Map(addressDTO, existingAddress);

                var updatedAddressResult = await _addressRepository.UpdateAddress(updatedAddress);
                var updatedAddressDTO = _mapper.Map<AddressDTO>(updatedAddressResult);

                return (updatedAddressDTO);
            } catch (Exception ex) {
                return ResultService.Fail<AddressDTO>($"Erro ao atualizar endereço: {ex.Message}");
            }
        }

        public async Task<object> DeleteAddress(Guid id) {
            try {
                var existingAddress = await _addressRepository.GetAddressById(id);

                if (existingAddress == null) {
                    return ResultService.Fail("Endereço não encontrado.");
                }

                await _addressRepository.DeleteAddress(existingAddress);

                return (existingAddress);
            } catch (Exception ex) {
                return ResultService.Fail($"Erro ao remover endereço: {ex.Message}");
            }
        }
    }
}