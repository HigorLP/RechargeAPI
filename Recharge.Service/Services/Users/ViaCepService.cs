using Recharge.Application.DTOs.Users;
using Recharge.Application.Integrations.ViaCep;
using Recharge.Application.Integrations.ViaCep.Refit;

namespace Recharge.Application.Services.Users;
public class ViaCepService : IViaCep {
    private readonly IViaCepRefit _viaCepRefit;

    public ViaCepService(IViaCepRefit viaCepRefit) {
        _viaCepRefit = viaCepRefit;
    }

    public async Task<AddressDTO> BuscarCep(string cep) {
        var response = await _viaCepRefit.BuscarDadosCep(cep);

        if (response != null && response.IsSuccessStatusCode) {

            var address = new AddressDTO {
                Cep = response.Content.Cep,
                Logradouro = response.Content.Logradouro,
                Complemento = response.Content.Complemento,
                Bairro = response.Content.Bairro,
                Localidade = response.Content.Localidade,
                Uf = response.Content.Uf
            };

            return address;
        }

        return null;
    }
}