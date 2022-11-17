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
            try
            {
                var products = await _repository.ProductRepository.GetAllAsync(cancellationToken);
                var productViews = _mapper.Map<List<ProductViewModel>>(products);

                return productViews;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<List<ProductViewModel>> GetListByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _repository.ProductRepository.GetListByIdsAsync(ids, cancellationToken);
                var productViews = _mapper.Map<List<ProductViewModel>>(products);

                return productViews;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<int> CreateAsync(ProductPostModel productPostModel, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(productPostModel);
                var id = await _repository.ProductRepository.CreateAsync(product, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return id;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<ProductViewModel> UpdateAsync(int id, ProductPutModel productPutModel, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                if (!id.Equals(productPutModel.Id))
                {
                    throw new ServiceException(ServiceErrorType.DifferentIds);
                }

                var isProductExist =
                    await _repository.ProductRepository.GetAnyAsync(product => product.Id.Equals(id),
                        cancellationToken);

                if (!isProductExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                    ;
                }

                var product = _mapper.Map<Product>(productPutModel);

                var updatedProduct = await _repository.ProductRepository.UpdateAsync(product);
                await _repository.SaveAsync(cancellationToken);

                return _mapper.Map<ProductViewModel>(updatedProduct);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<bool> DeleteAsync(int productId, CancellationToken cancellationToken)
        {
            try
            {
                if (productId <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                var isProductExist =
                    await _repository.ProductRepository.GetAnyAsync(product => product.Id.Equals(productId),
                        cancellationToken);

                if (!isProductExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                }

                var isProductDeleted = await _repository.ProductRepository.DeleteAsync(productId, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return isProductDeleted;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}
