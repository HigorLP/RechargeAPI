using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recharge.Domain.Models.Users;

namespace Recharge.Infra.Data.Maps.Users;
public class UserMap : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.HashPassword).IsRequired();

        builder.HasMany(x => x.Purchases).WithOne(c => c.User).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.Addresses).WithOne(c => c.User).HasForeignKey(x => x.UserId);
    }
}