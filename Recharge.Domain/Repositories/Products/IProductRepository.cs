using Recharge.Domain.Models.Products;

namespace Recharge.Domain.Repositories.Products;
public interface IProductRepository {
    Task<Product> CreateProduct(Product product);
    Task<Product> GetProductById(Guid id);
    Task<Product> GetProductByName(string productName);
    Task<Product> GetProductBySku(string sku);
    Task<Product> GetProductByBarCoode(string barCode);
    Task<ICollection<Product>> GetAllProducts();
    Task<ICollection<Product>> GetAllProductsInTheCategory(Guid categoryId);
    Task<ICollection<Product>> GetAllProductsInTheBrand(Guid brandId);
    Task<Product> UpdateProduct(Product product);
    Task<Product> RemoveProduct(Product product);
}