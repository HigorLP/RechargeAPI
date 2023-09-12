using Recharge.Domain.Models.Transactions;

namespace Recharge.Domain.Models.Products;
public sealed class Product : Entity {

    public string Name { get; set; }
    public string Sku { get; set; }
    public string BarCode { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public long? Amount { get; set; }
    public string? ImageUrl { get; set; }

    public Category Category { get; set; }
    public Guid? CategoryId { get; set; }

    public Brand Brand { get; set; }
    public Guid? BrandId { get; set; }

    public Datasheet Datasheet { get; set; }
    public Guid? DatasheetId { get; set; }

    public ICollection<CartItem> CartItems { get; set; }

    public Product(string name, string sku, string barCode, string? description, decimal? price, long? amount) {
        Name = name;
        Sku = sku;
        BarCode = barCode;
        Description = description;
        Price = price;
        Amount = amount;
        CartItems = new List<CartItem>();
    }

    public Product(decimal? price, long? amount) {
        Price = price;
        Amount = amount;
    }
}