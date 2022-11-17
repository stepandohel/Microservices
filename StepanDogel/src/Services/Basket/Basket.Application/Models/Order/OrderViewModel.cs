using Basket.Application.Models.Customer;
using Basket.Application.Models.Product;

namespace Basket.Application.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public CustomerViewModel CustomerView { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
