using AutoMapper;
using Catalog.Application.Models.Category;
using Catalog.Domain.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Queries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryReadModel>>
    {
        private readonly IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CategoryReadModel>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync(cancellationToken);

            return items;
        }
    }
}