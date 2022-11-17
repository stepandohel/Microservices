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
            var orders = await _repository.OrderRepository.GetAllAsync(cancellationToken, nameof(Order.Customer), nameof(Order.Products));

            var orderViews = _mapper.Map<List<OrderViewModel>>(orders);

            return orderViews;
        }

        public async Task<int> AddAsync(OrderPostModel orderPostModel, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(orderPostModel);

            decimal totalPrice = 0;
            var products = await _repository.ProductRepository.GetListByIdsAsync(orderPostModel.Products, cancellationToken);
            foreach (var product in products)
            {
                totalPrice += product.Price;
            }

            order.Products = products;
            order.Amount = totalPrice;

            var id = await _repository.OrderRepository.PostAsync(order, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return id;
        }

        public async Task<bool> UpdateAsync(int id, OrderPutModel orderPutModel, CancellationToken cancellationToken)
        {
            if (!id.Equals(orderPutModel.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }
            var item = await _repository.OrderRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var order = _mapper.Map<Order>(orderPutModel);

            decimal totalPrice = 0;
            var products = await _repository.ProductRepository.GetListByIdsAsync(orderPutModel.Products, cancellationToken);
            foreach (var product in products)
            {
                totalPrice += product.Price;
            }

            order.Products = products;
            order.Amount = totalPrice;

            var istrue = await _repository.OrderRepository.PutAsync(order, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }

        public async Task<bool> DeleteAsync(int orderId, CancellationToken cancellationToken)
        {
            var item = await _repository.OrderRepository.GetByIdAsync(orderId, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var istrue = await _repository.OrderRepository.DeleteAsync(orderId, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }

    }
}
