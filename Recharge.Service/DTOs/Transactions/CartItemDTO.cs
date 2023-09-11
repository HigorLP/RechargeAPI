namespace Recharge.Application.DTOs.Transactions;
public class CartItemDTO {
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid PurchaseId { get; set; }
    public int Amount { get; set; }
    public decimal PriceUn { get; set; }
}