using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Products;
public class CategoryRepository : ICategoryRepository {

    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateCategory(Category category) {
        _dbContext.Add(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> GetCategoryById(Guid id) {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category> GetCategoryByName(string name) {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<ICollection<Category>> GetAllCategories() {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category> UpdateCategory(Category category) {
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> DeleteCategory(Category category) {
        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }
}