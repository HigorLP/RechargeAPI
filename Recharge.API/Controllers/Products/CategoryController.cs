using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Products;
using Recharge.Application.Interfaces.Products;

namespace Recharge.API.Controllers.Products;

[Route("Category")]
[ApiController]
public class CategoryController : ControllerBase {
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService) {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO) {
        var result = await _categoryService.CreateCategory(categoryDTO);

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCategoryById(Guid id) {
        var result = await _categoryService.GetCategoryById(id);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("/{name}")]
    public async Task<ActionResult> GetCategoryByName(string name) {
        var result = await _categoryService.GetCategoryByName(name);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllCategories() {
        var result = await _categoryService.GetAllCategories();

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(Guid id, [FromBody] CategoryDTO categoryDTO) {
        var result = await _categoryService.UpdateCategory(id, categoryDTO);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id) {
        var result = await _categoryService.DeleteCategory(id);

        if (result != null) {
            return Ok(result);
        }

        return NotFound(result);
    }
}