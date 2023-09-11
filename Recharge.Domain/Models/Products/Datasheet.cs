namespace Recharge.Domain.Models.Products;
public sealed class Datasheet : Entity {

    public string Model { get; set; }
    public string Warranty { get; set; }

    public Product Product { get; set; }
    public Guid ProductId { get; set; }

    public Datasheet(string model, string warranty) {
        Model = model;
        Warranty = warranty;
    }
}