using Catalog.Domain.Data.Entities.Base;

namespace Catalog.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public double Cost { get; set; }
    }
}
