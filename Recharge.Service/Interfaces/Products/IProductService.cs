using Recharge.Application.DTOs.Products;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Products {
    public interface IProductService {
        Task<ResultService<ProductDTO>> CreateProduct(ProductDTO productDTO);
        Task<ResultService<ProductDTO>> GetProductById(Guid id);
        Task<ResultService<ProductDTO>> GetProductByName(string productName);
        Task<ResultService<ProductDTO>> GetProductBySku(string sku);
        Task<ResultService<ProductDTO>> GetProductByBarCode(string barCode);
        Task<ResultService<ICollection<ProductDTO>>> GetAllProducts();
        Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheCategory(Guid categoryId);
        Task<ResultService<ICollection<ProductDTO>>> GetAllProductsInTheBrand(Guid brandId);
        Task<ResultService<ProductDTO>> UpdateProduct(Guid id, ProductDTO productDTO);
        Task<ResultService<ProductDTO>> RemoveProduct(Guid id);
    }
}