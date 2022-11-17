using AutoMapper;
using Catalog.Application.Middleware.ServiceExceptions;
using Catalog.Application.Models.Category;
using Catalog.Domain.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryReadModel>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryReadModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
                }

                var category = _mapper.Map<Catalog.Domain.Entities.Category>(request);
                var updatedCategory = await _repository.CategoryRepository.UpdateAsync(category);
                await _repository.SaveAsync(cancellationToken);

                return _mapper.Map<CategoryReadModel>(updatedCategory);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

    }
}
