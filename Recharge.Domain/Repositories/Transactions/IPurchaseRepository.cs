using Recharge.Domain.Models.Transactions;

namespace Recharge.Domain.Repositories.Transactions; 
public interface IPurchaseRepository {
    Task<Purchase> CreatePurchase(Purchase purchase);
    Task<Purchase> GetPurchaseById(Guid id);
    Task<ICollection<Purchase>> GetPurchasesByUserId(Guid userId);
    Task<ICollection<Purchase>> GetAllPurchases();
    Task<Purchase> UpdatePurchase(Purchase purchase);
    Task<Purchase> RemovePurchase(Purchase purchase);
}