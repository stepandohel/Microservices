using Basket.Application.Models.Product;

namespace Basket.Infastructure.Services.Interfaces
{
    public interface IProductService : ICommonService<ProductViewModel, ProductPostModel, ProductPutModel>
    {
        public Task<List<ProductViewModel>> GetListByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
