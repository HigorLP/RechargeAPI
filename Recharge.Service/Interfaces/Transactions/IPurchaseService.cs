using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Transactions;
public interface IPurchaseService {
    Task<ResultService<PurchaseDTO>> CreatePurchase(PurchaseDTO purchaseDTO);
    Task<ResultService<PurchaseDTO>> GetPurchaseById(Guid id);
    Task<ResultService<ICollection<PurchaseDTO>>> GetPurchasesByUserId(Guid userId);
    Task<ResultService<ICollection<PurchaseDTO>>> GetAllPurchases();
    Task<ResultService<PurchaseDTO>> UpdatePurchase(Guid id, PurchaseDTO purchaseDTO);
    Task<ResultService<PurchaseDTO>> RemovePurchase(Guid id, PurchaseDTO purchaseDTO);
}