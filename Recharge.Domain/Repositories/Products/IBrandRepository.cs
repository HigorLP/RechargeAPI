using Recharge.Domain.Models.Products;

namespace Recharge.Domain.Repositories.Products;
public interface IBrandRepository {

    Task<Brand> CreateBrand(Brand brand);
    Task<Brand> GetBrandByName(string name);
    Task<Brand> GetBrandById(Guid id);
    Task<ICollection<Brand>> GetAllBrands();
    Task<Brand> UpdateBrand(Brand brand);
    Task<Brand> DeleteBrand(Brand brand);
}