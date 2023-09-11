using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;

namespace Recharge.API.Controllers.Products;

[Route("Datasheet")]
[ApiController]
public class DatasheetController : ControllerBase {
    private readonly IDatasheetService _datasheetService;

    public DatasheetController(IDatasheetService datasheetService) {
        _datasheetService = datasheetService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateDatasheet([FromBody] DatasheetDTO datasheetDTO) {
        var result = await _datasheetService.CreateDatasheet(datasheetDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDatasheetById(Guid id) {
        var result = await _datasheetService.GetDatasheetById(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllDatasheets() {
        var result = await _datasheetService.GetAllDatasheets();

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDatasheet(Guid id, [FromBody] DatasheetDTO datasheetDTO) {
        var result = await _datasheetService.UpdateDatasheet(id, datasheetDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDatasheet(Guid id) {
        var result = await _datasheetService.DeleteDatasheet(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }
}