using Catalog.Application.Models.Category;
using MediatR;

namespace Catalog.Application.Category.Commands.Update
{
    public class UpdateCategoryCommand: IRequest<CategoryReadModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
