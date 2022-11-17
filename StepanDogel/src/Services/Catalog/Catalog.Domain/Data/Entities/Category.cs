using Catalog.Domain.Data.Entities.Base;

namespace Catalog.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
