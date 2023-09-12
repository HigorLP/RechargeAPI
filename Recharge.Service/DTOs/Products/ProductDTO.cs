namespace Recharge.Application.DTOs.Products;
public class ProductDTO {

    public Guid? Id { get; set; }

    public string Name { get; set; }
    public string Sku { get; set; }
    public string BarCode { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public long? Amount { get; set; }
    public string? ImageUrl { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }
}