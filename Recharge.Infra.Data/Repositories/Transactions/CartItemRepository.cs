using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Repositories.Transactions;
using Recharge.Infra.Data.DataContext;

namespace Recharge.Infra.Data.Repositories.Transactions;
public class CartItemRepository : ICartItemRepository {

    private readonly ApplicationDbContext _dbContext;

    public CartItemRepository(ApplicationDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<CartItem> CreateCartItem(CartItem cartItem) {
        _dbContext.Add(cartItem);
        await _dbContext.SaveChangesAsync();
        return cartItem;
    }

    public async Task<ICollection<CartItem>> GetAllCartItems() {
        return await _dbContext.CartItems.ToListAsync();
    }

    public async Task<CartItem> GetCartItemById(Guid id) {
        return await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<CartItem>> GetCartItensByPurchase(Guid purchaseId) {
        return await _dbContext.CartItems.Where(p => p.PurchaseId == purchaseId).ToListAsync();
    }

    public async Task<CartItem> RemoveCartItem(CartItem cartItem) {
        _dbContext.Update(cartItem);
        await _dbContext.SaveChangesAsync();
        return cartItem;
    }
}