using Catalog.Domain.Data;
using Catalog.Domain.Entities;
using Catalog.Domain.Repository.Interfaces;

namespace Catalog.Domain.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
    }
}