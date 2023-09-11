using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recharge.Domain.Models.Products;

namespace Recharge.Infra.Data.Maps.Products; 
public class CategoryMap : IEntityTypeConfiguration<Category> {
    public void Configure(EntityTypeBuilder<Category> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.Products).WithOne(c => c.Category).HasForeignKey(x => x.CategoryId);
    }
}