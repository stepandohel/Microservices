using Identity.WebApi.Data;
using Identity.WebApi.IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.DI
{
    public static class WebAppExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication webApp)
        {

            using (var scope = webApp.Services.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<EfDbContext>().Database.MigrateAsync();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await RolesCreator.CreateRolesAsync(roleManager);
                await RolesCreator.CreateUsersAsync(userManager);

                await scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    await context.Database.MigrateAsync();

                    if (!context.Clients.Any())
                    {
                        foreach (var client in IdentityConfiguration.Clients)
                            await context.Clients.AddAsync(client.ToEntity());

                        await context.SaveChangesAsync();
                    }

                    if (!context.IdentityResources.Any())
                    {
                        foreach (var resource in IdentityConfiguration.IdentityResources)
                            await context.IdentityResources.AddAsync(resource.ToEntity());

                        await context.SaveChangesAsync();
                    }

                    if (!context.ApiScopes.Any())
                    {
                        foreach (var apiScope in IdentityConfiguration.ApiScopes)
                            await context.ApiScopes.AddAsync(apiScope.ToEntity());

                        await context.SaveChangesAsync();
                    }

                    if (!context.ApiResources.Any())
                    {
                        foreach (var resource in IdentityConfiguration.ApiResources)
                            await context.ApiResources.AddAsync(resource.ToEntity());

                        await context.SaveChangesAsync();
                    }
                }
                return webApp;
            }
        }
    }
}
