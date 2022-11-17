using Catalog.Domain.Data;

namespace Catalog.Domain.Repository.Interfaces
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CatalogDbContext _dbContext;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public RepositoryManager(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_dbContext);
                return _productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                _categoryRepository ??= new CategoryRepository(_dbContext);
                return (_categoryRepository);
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
