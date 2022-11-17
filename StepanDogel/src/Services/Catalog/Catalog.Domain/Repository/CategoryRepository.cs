using Catalog.Domain.Data;
using Catalog.Domain.Entities;
using Catalog.Domain.Repository.Interfaces;

namespace Catalog.Domain.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
