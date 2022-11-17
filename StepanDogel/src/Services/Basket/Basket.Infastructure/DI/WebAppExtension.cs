using Basket.Domain.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infastructure.DI
{
    public static class WebAppExtension
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                await context.Database.MigrateAsync();
            }

            return webApp;
        }
    }
}
