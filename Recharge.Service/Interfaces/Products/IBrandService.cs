using Recharge.Application.DTOs.Products;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Products;
public interface IBrandService {
    Task<ResultService<BrandDTO>> CreateBrand(BrandDTO brandDTO);
    Task<ResultService<BrandDTO>> GetBrandByName(string name);
    Task<ResultService<BrandDTO>> GetBrandById(Guid id);
    Task<ResultService<ICollection<BrandDTO>>> GetAllBrands();
    Task<ResultService<BrandDTO>> UpdateBrand(Guid id, BrandDTO brandDTO);
    Task<ResultService<BrandDTO>> DeleteBrand(Guid id);
}