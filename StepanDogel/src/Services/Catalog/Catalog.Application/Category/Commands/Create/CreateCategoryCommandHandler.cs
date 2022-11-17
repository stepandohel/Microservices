using AutoMapper;
using Catalog.Application.Models.Category;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Data.Entities;
using MediatR;

namespace Catalog.Application.Category.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryPostModel>(request);
            var id = await _repository.PostAsync(category, cancellationToken);

            return id;
        }
    }
}
