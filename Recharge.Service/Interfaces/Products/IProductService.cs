using Recharge.Application.DTOs.Products;

namespace Recharge.Application.Interfaces.Products {
    public interface IProductService {
        Task<object> CreateProduct(ProductDTO productDTO);
        Task<object> GetProductById(Guid id);
        Task<object> GetProductByName(string productName);
        Task<object> GetProductBySku(string sku);
        Task<object> GetProductByBarCode(string barCode);
        Task<ICollection<object>> GetAllProducts();
        Task<ICollection<object>> GetAllProductsInTheCategory(Guid categoryId);
        Task<ICollection<object>> GetAllProductsInTheBrand(Guid brandId);
        Task<object> UpdateProduct(Guid id, ProductDTO productDTO);
        Task<object> RemoveProduct(Guid id);
    }
}