using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain.Data
{
    public class CatalogDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }
    }
}
