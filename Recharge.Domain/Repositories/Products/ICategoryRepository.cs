using Recharge.Domain.Models.Products;

namespace Recharge.Domain.Repositories.Products;

public interface ICategoryRepository {

    Task<Category> CreateCategory(Category category);
    Task<Category> GetCategoryById(Guid id);
    Task<Category> GetCategoryByName(string name);
    Task<ICollection<Category>> GetAllCategories();
    Task<Category> UpdateCategory(Category category);
    Task<Category> DeleteCategory(Category category);
}