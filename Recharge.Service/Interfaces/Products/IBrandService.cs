using Recharge.Application.DTOs.Products;

namespace Recharge.Application.Interfaces.Products;
public interface IBrandService {
    Task<object> CreateBrand(BrandDTO brandDTO);
    Task<object> GetBrandByName(string name);
    Task<object> GetBrandById(Guid id);
    Task<ICollection<object>> GetAllBrands();
    Task<object> UpdateBrand(Guid id, BrandDTO brandDTO);
    Task<object> DeleteBrand(Guid id);
}