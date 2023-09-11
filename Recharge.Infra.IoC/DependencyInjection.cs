using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recharge.Application.Integrations.ViaCep;
using Recharge.Application.Integrations.ViaCep.Refit;
using Recharge.Application.Interfaces.Products;
using Recharge.Application.Interfaces.Transactions;
using Recharge.Application.Interfaces.Users;
using Recharge.Application.Mapping;
using Recharge.Application.Services.Products;
using Recharge.Application.Services.Transactions;
using Recharge.Application.Services.Users;
using Recharge.Application.Validator;
using Recharge.Application.Validator.Products;
using Recharge.Application.Validator.Transactions;
using Recharge.Application.Validator.User;
using Recharge.Application.Validators.Products;
using Recharge.Domain.Repositories.Products;
using Recharge.Domain.Repositories.Transactions;
using Recharge.Domain.Repositories.Users;
using Recharge.Infra.Data.Authentication;
using Recharge.Infra.Data.DataContext;
using Recharge.Infra.Data.Repositories.Products;
using Recharge.Infra.Data.Repositories.Transactions;
using Recharge.Infra.Data.Repositories.Users;
using Refit;

namespace Recharge.Infra.IoC;
public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Recharge.API")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDatasheetRepository, DatasheetRepository>();

        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddAutoMapper(typeof(DomainMapping));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDatasheetService, DatasheetService>();

        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<ICartItemService, CartItemService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAddressService, AddressService>();

        services.AddScoped<IViaCep, ViaCepService>();
        services.AddRefitClient<IViaCepRefit>().ConfigureHttpClient(c => {
            c.BaseAddress = new Uri("https://viacep.com.br");
        });

        services.AddScoped<CustomUserValidator>();
        services.AddScoped<UserValidator>();
        services.AddScoped<CategoryValidator>();
        services.AddScoped<BrandValidator>();
        services.AddScoped<ProductValidator>();
        services.AddScoped<DatasheetValidator>();
        services.AddScoped<PurchaseValidator>();

        services.AddScoped<TokenService>();
        services.AddHttpContextAccessor();
        return services;
    }
}