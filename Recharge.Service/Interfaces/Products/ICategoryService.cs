using Recharge.Application.DTOs.Products;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Products;
public interface ICategoryService {
    Task<ResultService<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO);
    Task<ResultService<CategoryDTO>> GetCategoryById(Guid id);
    Task<ResultService<CategoryDTO>> GetCategoryByName(string name);
    Task<ResultService<ICollection<CategoryDTO>>> GetAllCategories();
    Task<ResultService<CategoryDTO>> UpdateCategory(Guid id, CategoryDTO categoryDTO);
    Task<ResultService<CategoryDTO>> DeleteCategory(Guid id);
}