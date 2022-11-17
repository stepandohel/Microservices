using Microsoft.AspNetCore.Identity;

namespace Identity.WebApi.IdentityServer
{
    public class RolesCreator
    {
        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Visitor") is null)
            {
                await roleManager.CreateAsync(new IdentityRole("Visitor"));
            }

            if (await roleManager.FindByNameAsync("Admin") is null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        public static async Task CreateUsersAsync(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@gmail.com") is null)
            {
                var admin = new IdentityUser
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com"
                };

                await userManager.CreateAsync(admin, "AdminPassword");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
