using AutoMapper;
using Catalog.Application.Middleware.ServiceExceptions;
using Catalog.Application.Models.Category;
using Catalog.Domain.Repository.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Queries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryReadModel>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CategoryReadModel>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _repository.CategoryRepository.GetAllAsync(cancellationToken);

                return _mapper.Map<List<CategoryReadModel>>(categories);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}