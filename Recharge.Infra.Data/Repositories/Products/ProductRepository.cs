using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Products; 
public class ProductRepository : IProductRepository {

    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Product> CreateProduct(Product product) {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<ICollection<Product>> GetAllProducts() {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product> GetProductByBarCoode(string barCode) {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.BarCode == barCode);
    }

    public async Task<Product> GetProductById(Guid id) {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> GetProductByName(string productName) {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == productName);
    }

    public async Task<Product> GetProductBySku(string sku) {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Sku == sku);
    }

    public async Task<Product> RemoveProduct(Product product) {
        _dbContext.Remove(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product) {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<ICollection<Product>> GetAllProductsInTheCategory(Guid categoryId) {
        return await _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<ICollection<Product>> GetAllProductsInTheBrand(Guid brandId) {
        return await _dbContext.Products.Where(p => p.BrandId == brandId).ToListAsync();
    }
}