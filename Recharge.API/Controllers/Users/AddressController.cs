using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Users;
using Recharge.Application.Integrations.ViaCep;
using Recharge.Application.Interfaces.Users;

[Route("Address")]
[ApiController]
public class AddressController : ControllerBase {
    private readonly IViaCep _viaCep;
    private readonly IAddressService _addressService;

    public AddressController(IViaCep viaCep, IAddressService addressService) {
        _viaCep = viaCep;
        _addressService = addressService;
    }

    [HttpGet("viacep/{cep}")]
    public async Task<ActionResult<AddressDTO>> BuscarEndereco(string cep) {
        var address = await _viaCep.BuscarCep(cep);

        if (address is null)
            return BadRequest("CEP não encontrado.");

        return Ok(address);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAddress([FromBody] AddressDTO addressDTO) {
        var result = await _addressService.CreateAddress(addressDTO);

        if (result == null) {
            return BadRequest("Erro ao criar endereço.");
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAddressById(Guid id) {
        var result = await _addressService.GetAddressById(id);

        if (result == null) {
            return NotFound("Endereço não encontrado.");
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAddress([FromBody] AddressDTO addressDTO) {
        var result = await _addressService.GetAddress(addressDTO);

        if (result == null) {
            return BadRequest("Erro ao obter endereços.");
        }

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAddress([FromBody] AddressDTO addressDTO) {
        var result = await _addressService.UpdateAddress(addressDTO);

        if (result == null) {
            return BadRequest("Erro ao atualizar endereço.");
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAddress(Guid id) {
        var result = await _addressService.DeleteAddress(id);

        if (result == null) {
            return BadRequest("Erro ao remover endereço.");
        }

        return Ok(result);
    }
}