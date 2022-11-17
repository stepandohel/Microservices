using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Data
{
    public class EfDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }
    }
}
