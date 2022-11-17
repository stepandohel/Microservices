using AutoMapper;
using Catalog.Application.Middleware.ServiceExceptions;
using Catalog.Domain.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _mapper.Map<Catalog.Domain.Entities.Category>(request);
                var createdCategoryId = await _repository.CategoryRepository.CreateAsync(category, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return createdCategoryId;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}
