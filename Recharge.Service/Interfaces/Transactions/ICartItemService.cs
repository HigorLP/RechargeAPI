using Recharge.Application.DTOs.Transactions;

namespace Recharge.Application.Interfaces.Transactions;
public interface ICartItemService {
    Task<object> CreateCartItem(CartItemDTO cartItemDTO);
    Task<object> GetCartItemById(Guid id);
    Task<ICollection<object>> GetCartItensByPurchase(Guid purchaseId);
    Task<ICollection<object>> GetAllCartItems();
    Task<object> RemoveCartItem(Guid id, CartItemDTO cartItemDTO);
}