using Recharge.Application.DTOs.Products;

namespace Recharge.Application.Interfaces.Products;
public interface ICategoryService {
    Task<object> CreateCategory(CategoryDTO categoryDTO);
    Task<object> GetCategoryById(Guid id);
    Task<object> GetCategoryByName(string name);
    Task<ICollection<object>> GetAllCategories();
    Task<object> UpdateCategory(Guid id, CategoryDTO categoryDTO);
    Task<object> DeleteCategory(Guid id);
}