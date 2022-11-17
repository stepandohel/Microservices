using Basket.Domain.Data;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;

namespace Basket.Infastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BasketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
