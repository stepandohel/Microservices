using AutoMapper;
using Catalog.Application.Models.Category;
using Catalog.Domain.Interfaces;
using MediatR;

namespace Catalog.Application.Category.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IBaseRepository<Domain.Entities.Category, CategoryReadModel, CategoryPostModel, CategoryPutModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryPutModel>(request);
            var isTrue = await _repository.PutAsync(category, cancellationToken);

            return isTrue;
        }

    }
}
