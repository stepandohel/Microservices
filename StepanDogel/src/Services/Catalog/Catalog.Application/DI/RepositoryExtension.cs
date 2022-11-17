using Catalog.Domain.Data;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DI
{
    public static class RepositoryExtension
    {

        public static IServiceCollection AddContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<CatalogDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(IBaseRepository<>).Assembly.FullName)
            ));
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }
    }
}
