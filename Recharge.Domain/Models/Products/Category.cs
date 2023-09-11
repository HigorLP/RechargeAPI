namespace Recharge.Domain.Models.Products;
public sealed class Category : Entity {

    public string Name { get; private set; }

    public ICollection<Product> Products { get; set; }

    public Category(string name) {
        Name = name;

        Products = new List<Product>();
    }
}