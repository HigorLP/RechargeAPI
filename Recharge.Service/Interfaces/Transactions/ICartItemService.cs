using Recharge.Application.DTOs.Transactions;
using Recharge.Application.Services;

namespace Recharge.Application.Interfaces.Transactions;
public interface ICartItemService {
    Task<ResultService<CartItemDTO>> CreateCartItem(CartItemDTO cartItemDTO);
    Task<ResultService<CartItemDTO>> GetCartItemById(Guid id);
    Task<ResultService<ICollection<CartItemDTO>>> GetCartItensByPurchase(Guid purchaseId);
    Task<ResultService<CartItemDTO>> RemoveCartItem(Guid id, CartItemDTO cartItemDTO);
}