using Microsoft.EntityFrameworkCore;
using Recharge.Domain.Models.Products;
using Recharge.Domain.Models.Transactions;
using Recharge.Domain.Models.Users;

namespace Recharge.Infra.Data.DataContext;

public class ApplicationDbContext : DbContext {

    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Datasheet> Datasheets { get; set; }

    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}