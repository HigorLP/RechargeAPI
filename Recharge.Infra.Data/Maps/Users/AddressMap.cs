using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recharge.Domain.Models.Users;

namespace Recharge.Infra.Data.Maps.Users;
public class AddressMap : IEntityTypeConfiguration<Address> {
    public void Configure(EntityTypeBuilder<Address> builder) {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Cep).IsRequired();
        builder.Property(x => x.Logradouro).IsRequired();
        builder.Property(x => x.Bairro).IsRequired();
        builder.Property(x => x.Localidade).IsRequired();
        builder.Property(x => x.Uf).IsRequired();

        builder.HasOne(x => x.User).WithMany(c => c.Addresses).HasForeignKey(x => x.UserId);
    }
}