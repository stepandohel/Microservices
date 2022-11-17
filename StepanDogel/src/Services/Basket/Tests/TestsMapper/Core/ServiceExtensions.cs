using Basket.Application.Mapping;
using Basket.Domain.Data;
using Basket.Infastructure.Repository;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services;
using Basket.Infastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestsMapper.Core
{
    internal static class ServiceExtensions
    {
        public static ServiceProvider SetupServiceProvider(this ServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = config.GetConnectionString("TestDatabase");

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddDbContext<BasketDbContext>(option => option.UseNpgsql(connectionString,
                x => x.MigrationsAssembly(typeof(IBaseRepository<>).Assembly.FullName)));
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();

            serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                context.Database.Migrate();
            }

            return serviceProvider;
        }

        public static void DeleteDatabase(this ServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                context.Database.CloseConnection();
                context.Database.EnsureDeleted();
            }
        }
    }
}
