namespace Recharge.Domain.Models.Products;

public sealed class Brand : Entity {

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }

    public Brand(string name) {
        Name = name;
        Products = new List<Product>();
    }
}