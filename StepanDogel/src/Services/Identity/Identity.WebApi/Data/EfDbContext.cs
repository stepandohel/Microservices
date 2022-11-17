using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.Data
{
    public class EfDbContext : IdentityDbContext<IdentityUser>
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        public EfDbContext()
        {

        }
    }
}
