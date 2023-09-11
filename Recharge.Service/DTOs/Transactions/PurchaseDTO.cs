namespace Recharge.Application.DTOs.Transactions;
public class PurchaseDTO {
    public Guid? Id { get; set; }
    public string Payment { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
    public ICollection<CartItemDTO> CartItems { get; set; }
}