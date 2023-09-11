using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Repositories.Products;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Products;
public class DatasheetRepository : IDatasheetRepository {

    private readonly ApplicationDbContext _dbContext;

    public DatasheetRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Datasheet> CreateDatasheet(Datasheet datasheet) {
        _dbContext.Add(datasheet);
        await _dbContext.SaveChangesAsync();
        return datasheet;
    }

    public async Task<Datasheet> GetDatasheetById(Guid id) {
        return await _dbContext.Datasheets.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Datasheet>> GetAllDatasheets() {
        return await _dbContext.Datasheets.ToListAsync();
    }

    public async Task<Datasheet> UpdateDatasheet(Datasheet datasheet) {
        _dbContext.Update(datasheet);
        await _dbContext.SaveChangesAsync();
        return datasheet;
    }

    public async Task<Datasheet> DeleteDatasheet(Datasheet datasheet) {
        _dbContext.Remove(datasheet);
        await _dbContext.SaveChangesAsync();
        return datasheet;
    }
}