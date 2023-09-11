using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Transactions;

namespace Recharge.Infra.Data.Maps.Transactions;
public class PurchaseMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BuyDate).IsRequired();

        builder.Property(x => x.Payment).IsRequired();

        builder.Property(x => x.Status).IsRequired();

        builder.HasMany(x => x.CartItems).WithOne(c => c.Purchase).HasForeignKey(x => x.PurchaseId);
    }
}