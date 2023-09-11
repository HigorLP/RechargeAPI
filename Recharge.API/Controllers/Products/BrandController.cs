using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;

namespace Recharge.API.Controllers.Products;

[Route("Brand")]
[ApiController]
public class BrandController : ControllerBase {
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService) {
        _brandService = brandService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBrand([FromBody] BrandDTO brandDTO) {
        var result = await _brandService.CreateBrand(brandDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBrandById(Guid id) {
        var result = await _brandService.GetBrandById(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetBrandByName(string name) {
        var result = await _brandService.GetBrandByName(name);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllBrands() {
        var result = await _brandService.GetAllBrands();

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBrand(Guid id, [FromBody] BrandDTO brandDTO) {
        var result = await _brandService.UpdateBrand(id, brandDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrand(Guid id) {
        var result = await _brandService.DeleteBrand(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }
}