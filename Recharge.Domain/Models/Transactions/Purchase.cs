using Recharge.Domain.Models.Users;

namespace Recharge.Domain.Models.Transactions;
public sealed class Purchase : Entity {

    public DateTime BuyDate { get; set; }
    public string Payment { get; set; }
    public string Status { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public ICollection<CartItem> CartItems { get; set; }

    public Purchase(DateTime buyDate, string payment, string status) {
        BuyDate = buyDate;
        Payment = payment;
        Status = status;
        CartItems = new List<CartItem>();
    }

    public Purchase() { }
}