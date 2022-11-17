using AutoMapper;
using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Application.Models.Product;
using Basket.Domain.Data.Entities;

namespace Basket.Application.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<CustomerPostModel, Customer>();
            CreateMap<CustomerPutModel, Customer>();

            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductPostModel, Product>();
            CreateMap<ProductPutModel, Product>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerView, opt => opt.MapFrom(x => x.Customer));
            CreateMap<OrderViewModel, Order>();
            CreateMap<OrderPostModel, Order>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<OrderPutModel, Order>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());

        }
    }
}
