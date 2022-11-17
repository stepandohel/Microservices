using Basket.Domain.Data.Entities.Base;

namespace Basket.Domain.Data.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
