using Basket.Domain.Data;
using Basket.Infastructure.Repository.Interfaces;

namespace Basket.Infastructure.Repository
{
    internal class RepositoryManager : IRepositoryManager
    {
        private BasketDbContext _dbContext;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext);
                }

                return _productRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_dbContext);
                }

                return (_customerRepository);
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_dbContext);
                }

                return (_orderRepository);
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
