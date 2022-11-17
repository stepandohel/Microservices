using MediatR;

namespace Catalog.Application.Category.Commands.Update
{
    public class UpdateCategoryCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
