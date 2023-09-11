using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;

namespace Recharge.Infra.Data.Maps.Products;
public class DatasheetMap : IEntityTypeConfiguration<Datasheet> {
    public void Configure(EntityTypeBuilder<Datasheet> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Model).IsRequired();

        builder.Property(x => x.Warranty).IsRequired();

        builder.HasOne(x => x.Product).WithOne(c => c.Datasheet).HasForeignKey<Product>(x => x.DatasheetId);
    }
}