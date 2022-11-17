using Basket.Domain.Data;
using Basket.Infastructure.Repository;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infastructure.DI
{
    public static class RepositoryExtensions
    {
        public static IMvcBuilder AddServices(this IMvcBuilder services)
        {
            services.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.Services.AddScoped<ICustomerService, CustomerService>();
            services.Services.AddScoped<IOrderService, OrderService>();
            services.Services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<BasketDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(IBaseRepository<>).Assembly.FullName)
            ));

            return services;
        }
    }
}
