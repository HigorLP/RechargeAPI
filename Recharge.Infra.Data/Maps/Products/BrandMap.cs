using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recharge.Domain.Models.Products;

namespace Recharge.Infra.Data.Maps.Products; 
public class BrandMap : IEntityTypeConfiguration<Brand> {
    public void Configure(EntityTypeBuilder<Brand> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.Products).WithOne(c => c.Brand).HasForeignKey(x => x.BrandId);
    }
}