using Catalog.Application.Models.Category;
using MediatR;

namespace Catalog.Application.Category.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryReadModel>>
    {
    }
}
