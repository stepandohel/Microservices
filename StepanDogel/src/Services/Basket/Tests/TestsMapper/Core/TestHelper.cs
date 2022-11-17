using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Application.Models.Product;
using Basket.Domain.Data.Entities;

namespace TestsMapper.Core
{
    public static class TestHelper
    {
        public static Customer CreateEntity(
            string Name,
            string Surname,
            string Adress
        )
        {
            var createModel = new Customer()
            {
                Name = Name,
                Surname = Surname,
                Address = Adress
            };
            return createModel;
        }
        public static Customer CreateBaseCustomerEntity()
        {
            var createModel = new Customer()
            {
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };
            return createModel;
        }
        public static Product CreateBaseProductEntity()
        {
            var createModel = new Product()
            {
                Price = 20
            };
            return createModel;
        }
        public static Order CreateBaseOrderEntity()
        {
            var createModel = new Order()
            {
                Amount = 50,
                CustomerId = 1,
                Products = new List<Product>()
                 {
                     new Product()
                     {
                         Id = 1,
                         Price = 20
                     },
                     new Product()
                     {
                         Id = 2,
                         Price = 30
                     }
                 }
            };
            return createModel;
        }

        public static CustomerViewModel CreateBaseViewModel()
        {
            var createModel = new CustomerViewModel()
            {
                Id = 1,
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };
            return createModel;
        }
        public static ProductViewModel CreateBaseProductViewModel()
        {
            var createModel = new ProductViewModel()
            {
                Id = 1,
                Price = 20
            };
            return createModel;
        }
        public static OrderViewModel CreateBaseOrderViewModel()
        {
            var createModel = new OrderViewModel()
            {
                Id = 1,
                Amount = 50,
                CustomerView = new CustomerViewModel()
                {
                    Id = 1,
                    Address = "random Street",
                    Name = "Name",
                    Surname = "Surname"
                },
                Products = new List<ProductViewModel>()
                {
                    new ProductViewModel()
                    {
                        Id = 1,
                        Price = 20
                    },
                    new ProductViewModel()
                    {
                        Id = 2,
                        Price = 30
                    }
                }
            };
            return createModel;
        }
        public static CustomerViewModel GetModelDto(
            string Name,
            string Surname,
            string Adress)
        {
            return new CustomerViewModel()
            {
                Id = 52,
                Name = Name,
                Surname = Surname,
                Address = Adress
            };
        }
    }
}