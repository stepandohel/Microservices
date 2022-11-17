using MediatR;

namespace Catalog.Application.Category.Commands.Create
{
    public class CreateCategoryCommand: IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
