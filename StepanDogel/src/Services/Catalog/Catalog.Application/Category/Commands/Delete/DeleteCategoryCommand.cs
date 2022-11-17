using MediatR;

namespace Catalog.Application.Category.Commands.Delete
{
    public class DeleteCategoryCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
