using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Products;
public class BrandRepository : IBrandRepository {

    private readonly ApplicationDbContext _dbContext;

    public BrandRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Brand> CreateBrand(Brand brand) {
        _dbContext.Add(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> GetBrandById(Guid id) {
        return await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Brand> GetBrandByName(string name) {
        return await _dbContext.Brands.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<ICollection<Brand>> GetAllBrands() {
        return await _dbContext.Brands.ToListAsync();
    }

    public async Task<Brand> UpdateBrand(Brand brand) {
        _dbContext.Update(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> DeleteBrand(Brand brand) {
        _dbContext.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return brand;
    }
}