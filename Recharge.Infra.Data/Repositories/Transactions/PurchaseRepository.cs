using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Repositories.Transactions;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Transactions; 
public class PurchaseRepository : IPurchaseRepository {

    private readonly ApplicationDbContext _dbContext;

    public PurchaseRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<Purchase> CreatePurchase(Purchase purchase) {
        _dbContext.Add(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }

    public async Task<ICollection<Purchase>> GetAllPurchases() {
        return await _dbContext.Purchases.ToListAsync();
    }

    public async Task<Purchase> GetPurchaseById(Guid id) {
        return await _dbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Purchase>> GetPurchasesByUserId(Guid userId) {
        return await _dbContext.Purchases.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Purchase> RemovePurchase(Purchase purchase) {
        _dbContext.Remove(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> UpdatePurchase(Purchase purchase) {
        _dbContext.Update(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }
}