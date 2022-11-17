using AutoMapper;
using Basket.Application.Middleware.Exceptions;
using Basket.Application.Middleware.ServiceExceptions;
using Basket.Application.Models.Customer;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services.Interfaces;

namespace Basket.Infastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomerViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _repository.CustomerRepository.GetAllAsync(cancellationToken);

            var customerViews = _mapper.Map<List<CustomerViewModel>>(customers);

            return customerViews;
        }

        public async Task<int> AddAsync(CustomerPostModel customerPostModel, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(customerPostModel);

            var id = await _repository.CustomerRepository.PostAsync(customer, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return id;
        }

        public async Task<bool> UpdateAsync(int id, CustomerPutModel customerPutModel, CancellationToken cancellationToken)
        {
            if (!id.Equals(customerPutModel.Id))
            {
                throw new ServiceException(ServiceErrorType.DifferentIds);
            }
            var item = await _repository.CustomerRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var customer = _mapper.Map<Customer>(customerPutModel);

            var istrue = await _repository.CustomerRepository.PutAsync(customer, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }

        public async Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
        {
            var item = await _repository.CustomerRepository.GetByIdAsync(customerId, cancellationToken);
            if (item == null)
            {
                throw new ServiceException(ServiceErrorType.NoEntity); ;
            }

            var istrue = await _repository.CustomerRepository.DeleteAsync(customerId, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return istrue;
        }
    }
}
