using Recharge.Application.DTOs.Transactions;

namespace Recharge.Application.Interfaces.Transactions;
public interface IPurchaseService {
    Task<object> CreatePurchase(PurchaseDTO purchaseDTO);
    Task<object> GetPurchaseById(Guid id);
    Task<ICollection<object>> GetPurchasesByUserId(Guid userId);
    Task<ICollection<object>> GetAllPurchases();
    Task<object> UpdatePurchase(Guid id, PurchaseDTO purchaseDTO);
    Task<object> RemovePurchase(Guid id, PurchaseDTO purchaseDTO);
}