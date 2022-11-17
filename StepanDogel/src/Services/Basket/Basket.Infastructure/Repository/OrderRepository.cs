using Basket.Domain.Data;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;

namespace Basket.Infastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BasketDbContext dbContext) : base(dbContext)
        {
        }

    }
}
