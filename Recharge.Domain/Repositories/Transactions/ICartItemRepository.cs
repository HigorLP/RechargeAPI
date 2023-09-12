using Recharge.Domain.Models.Transactions;

namespace Recharge.Domain.Repositories.Transactions;
public interface ICartItemRepository {
    Task<CartItem> CreateCartItem(CartItem cartItem);
    Task<CartItem> GetCartItemById(Guid id);
    Task<ICollection<CartItem>> GetCartItensByPurchase(Guid purchaseId);
    Task<ICollection<CartItem>> GetAllCartItems();
    Task<CartItem> RemoveCartItem(CartItem cartItem);
}