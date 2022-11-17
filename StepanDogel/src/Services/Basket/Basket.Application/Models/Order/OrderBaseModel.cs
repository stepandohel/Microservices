namespace Basket.Application.Models.Order
{
    public class OrderBaseModel
    {
        public int CustomerId { get; set; }
        public List<int> Products { get; set; }
    }
}
