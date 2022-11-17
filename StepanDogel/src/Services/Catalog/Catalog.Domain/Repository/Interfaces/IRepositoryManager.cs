namespace Catalog.Domain.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task SaveAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
