using AutoMapper;
using Basket.Application.Middleware.Exceptions;
using Basket.Application.Middleware.ServiceExceptions;
using Basket.Application.Models.Order;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services.Interfaces;

namespace Basket.Infastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrderViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _repository.OrderRepository.GetAllAsync(cancellationToken, nameof(Order.Customer),
                    nameof(Order.Products));
                var orderViews = _mapper.Map<List<OrderViewModel>>(orders);

                return orderViews;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<int> CreateAsync(OrderPostModel orderPostModel, CancellationToken cancellationToken)
        {
            try
            {
                var order = _mapper.Map<Order>(orderPostModel);

                decimal totalPrice = 0;
                var products =
                    await _repository.ProductRepository.GetListByIdsAsync(orderPostModel.Products, cancellationToken);

                foreach (var product in products)
                {
                    totalPrice += product.Price;
                }

                order.Products = products;
                order.Amount = totalPrice;

                var id = await _repository.OrderRepository.CreateAsync(order, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return id;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<OrderViewModel> UpdateAsync(int id, OrderPutModel orderPutModel, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                if (!id.Equals(orderPutModel.Id))
                {
                    throw new ServiceException(ServiceErrorType.DifferentIds);
                }

                var isOrderExist =
                    await _repository.OrderRepository.GetAnyAsync(order => order.Id.Equals(id), cancellationToken);

                if (isOrderExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                    ;
                }

                var order = _mapper.Map<Order>(orderPutModel);

                decimal totalPrice = 0;
                var products =
                    await _repository.ProductRepository.GetListByIdsAsync(orderPutModel.Products, cancellationToken);

                foreach (var product in products)
                {
                    totalPrice += product.Price;
                }

                order.Products = products;
                order.Amount = totalPrice;

                var updatedOrder = await _repository.OrderRepository.UpdateAsync(order);
                await _repository.SaveAsync(cancellationToken);

                return _mapper.Map<OrderViewModel>(updatedOrder);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                if (orderId <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                var isOrderExist =
                    await _repository.OrderRepository.GetAnyAsync(order => order.Id.Equals(orderId), cancellationToken);

                if (!isOrderExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                    ;
                }

                var isOrderDeleted = await _repository.OrderRepository.DeleteAsync(orderId, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return isOrderDeleted;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}
