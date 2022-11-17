using Basket.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basket.Domain.Data
{
    public class BasketDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }
    }
}
