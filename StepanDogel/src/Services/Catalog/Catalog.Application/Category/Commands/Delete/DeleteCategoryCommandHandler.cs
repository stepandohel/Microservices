using Catalog.Application.Middleware.ServiceExceptions;
using Catalog.Domain.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IRepositoryManager _repository;

        public DeleteCategoryCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                var isCategoryExist =
                    await _repository.CategoryRepository.GetAnyAsync(customer => customer.Id.Equals(request.Id),
                        cancellationToken);

                if (!isCategoryExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                    ;
                }

                var isCategoryDeleted = await _repository.CategoryRepository.DeleteAsync(request.Id, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return isCategoryDeleted;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}
