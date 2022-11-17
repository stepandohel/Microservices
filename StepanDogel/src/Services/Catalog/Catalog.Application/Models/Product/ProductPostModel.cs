namespace Catalog.Application.Models.Product
{
    public class ProductPostModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public double Cost { get; set; }
    }
}
