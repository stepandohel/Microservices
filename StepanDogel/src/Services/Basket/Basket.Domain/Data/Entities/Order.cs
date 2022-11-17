using Basket.Domain.Data.Entities.Base;

namespace Basket.Domain.Data.Entities
{
    public class Order : BaseEntity
    {
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
