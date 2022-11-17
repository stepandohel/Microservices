using Basket.Application.Models.Order;

namespace Basket.Infastructure.Services.Interfaces
{
    public interface IOrderService : ICommonService<OrderViewModel, OrderPostModel, OrderPutModel>
    {
    }
}
