using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recharge.Domain.Models.Products;

namespace Recharge.Infra.Data.Maps.Products;
public class ProductMap : IEntityTypeConfiguration<Product> {
    public void Configure(EntityTypeBuilder<Product> builder) {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Amount).IsRequired();

        builder.Property(x => x.Sku).IsRequired();

        builder.Property(x => x.BarCode).IsRequired();

        builder.Property(x => x.Price).IsRequired();

        builder.HasOne(x => x.Datasheet).WithOne(c => c.Product).HasForeignKey<Datasheet>(x => x.ProductId);
    }
}