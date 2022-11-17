using Basket.Infastructure.Repository;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infastructure.Ioc
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
    }
}
