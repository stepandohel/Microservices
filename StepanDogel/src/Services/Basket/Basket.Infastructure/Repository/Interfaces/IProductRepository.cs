using Basket.Domain.Data.Entities;

namespace Basket.Infastructure.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<List<Product>> GetListByIdsAsync(List<int> ids, CancellationToken token);
    }
}
