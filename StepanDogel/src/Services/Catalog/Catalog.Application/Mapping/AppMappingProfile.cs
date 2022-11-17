using AutoMapper;
using Catalog.Application.Category.Commands.Create;
using Catalog.Application.Category.Commands.Update;
using Catalog.Application.Models.Category;
using Catalog.Application.Models.Product;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Product, ProductReadModel>().ReverseMap();
            CreateMap<ProductPostModel, Product>();
            CreateMap<ProductPutModel, Product>();

            CreateMap<Domain.Entities.Category, CategoryReadModel>().ReverseMap();
            CreateMap<CategoryCreateModel, Domain.Entities.Category>();
            CreateMap<CategoryUpdateModel, Domain.Entities.Category>();
            CreateMap<CreateCategoryCommand, Domain.Entities.Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Domain.Entities.Category>().ReverseMap();
        }
    }
}
