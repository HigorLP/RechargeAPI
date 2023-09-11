using Recharge.Application.Integrations.ViaCep.Response;
using Refit;

namespace Recharge.Application.Integrations.ViaCep.Refit;

public interface IViaCepRefit {
    [Get("/ws/{cep}/json")]
    Task<ApiResponse<ViaCepResponse>> BuscarDadosCep(string cep);
}