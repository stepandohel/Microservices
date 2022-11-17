using Catalog.Application.Models.Category;

namespace Catalog.Application.Models.Product
{
    public class ProductReadModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryReadModel Category { get; set; }
        public double Cost { get; set; }
    }
}
