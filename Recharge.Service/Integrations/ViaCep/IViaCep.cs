using Recharge.Application.DTOs.Users;

namespace Recharge.Application.Integrations.ViaCep;
public interface IViaCep {
    Task<AddressDTO> BuscarCep(string cep);
}