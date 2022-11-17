using Basket.Application.Models.Customer;

namespace Basket.Infastructure.Services.Interfaces
{
    public interface ICustomerService : ICommonService<CustomerViewModel, CustomerCreateModel, CustomerUpdateModel>
    {
    }
}
