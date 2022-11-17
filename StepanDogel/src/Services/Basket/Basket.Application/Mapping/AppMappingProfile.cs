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
            CreateMap<CustomerCreateModel, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
            CreateMap<CustomerUpdateModel, Customer>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore());

            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductPostModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
            CreateMap<ProductPutModel, Product>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore());

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerView, opt => opt.MapFrom(x => x.Customer));
            CreateMap<OrderPostModel, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
            CreateMap<OrderPutModel, Order>()
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
        }
    }
}
