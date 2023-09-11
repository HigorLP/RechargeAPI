using Recharge.Domain.Models.Products;

namespace Recharge.Domain.Models.Transactions;

public sealed class CartItem : Entity {

    public int Amount { get; set; }
    public decimal PriceUn { get; set; }

    public Product Product { get; set; }
    public Guid ProductId { get; set; }

    public Purchase Purchase { get; set; }
    public Guid PurchaseId { get; set; }

    public CartItem(int amount, decimal priceUn) {
        Amount = amount;
        PriceUn = priceUn;
    }
}