using AutoMapper;
using Basket.Application.Middleware.Exceptions;
using Basket.Application.Middleware.ServiceExceptions;
using Basket.Application.Models.Product;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services.Interfaces;

namespace Basket.Infastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.ProductRepository.GetAllAsync(cancellationToken);

            var productViews = _mapper.Map<List<ProductViewModel>>(products);

            return productViews;
        }

        public async Task<List<ProductViewModel>> GetListByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            var products = await _repository.ProductRepository.GetListByIdsAsync(ids, cancellationToken);

            var productViews = _mapper.Map<List<ProductViewModel>>(products);

            return productViews;
        }

        public async Task<int> AddAsync(ProductPostModel productPostModel, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productPostModel);

            var id = await _repository.ProductRepository.PostAsync(product, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return id;
        }

        public async Task<bool> UpdateAsync(int id, ProductPutModel productPutModel, CancellationToken cancellationToken)
        {
            if (!id.Equals(productPutModel.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }
            var item = await _repository.ProductRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var product = _mapper.Map<Product>(productPutModel);

            var istrue = await _repository.ProductRepository.PutAsync(product, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }

        public async Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken)
        {
            var item = await _repository.ProductRepository.GetByIdAsync(productId, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var istrue = await _repository.ProductRepository.DeleteAsync(productId, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }
    }
}
