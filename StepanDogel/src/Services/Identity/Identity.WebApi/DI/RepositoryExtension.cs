using Identity.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.DI
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EfDbContext>(option => option.UseSqlServer(connectionString,
                x => x.MigrationsAssembly(typeof(EfDbContext).Assembly.FullName)
            ));

            return services;
        }
    }
}
