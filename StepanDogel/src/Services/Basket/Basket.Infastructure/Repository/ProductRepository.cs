using Basket.Domain.Data;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BasketDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetListByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            var items = await _dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
            
            return items;
        }
    }
}
