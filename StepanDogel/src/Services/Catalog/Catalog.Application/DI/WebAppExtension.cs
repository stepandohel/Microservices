using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.DI
{
    public static class WebAppExtension
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Catalog.Domain.Data.CatalogDbContext>();
                await context.Database.MigrateAsync();
            }

            return webApp;
        }
    }
}
