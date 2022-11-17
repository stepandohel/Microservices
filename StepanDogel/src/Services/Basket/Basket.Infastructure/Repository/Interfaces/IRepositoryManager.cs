namespace Basket.Infastructure.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
