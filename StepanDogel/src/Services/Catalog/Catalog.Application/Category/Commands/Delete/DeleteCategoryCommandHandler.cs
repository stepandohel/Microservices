using AutoMapper;
using Catalog.Application.Models.Category;
using Catalog.Domain.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Commands.Delete
{
    public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> _repository;

        public DeleteCategoryCommandHandler(IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var isTrue = await _repository.DeleteAsync(request.Id, cancellationToken);

            return isTrue;
        }
    }
}
