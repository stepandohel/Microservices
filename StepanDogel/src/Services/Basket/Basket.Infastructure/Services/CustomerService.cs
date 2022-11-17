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
            try
            {
                var customers = await _repository.CustomerRepository.GetAllAsync(cancellationToken);
                var customerViews = _mapper.Map<List<CustomerViewModel>>(customers);

                return customerViews;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<int> CreateAsync(CustomerCreateModel customerPostModel, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerPostModel);
                var id = await _repository.CustomerRepository.CreateAsync(customer, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return id;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }

        public async Task<CustomerViewModel> UpdateAsync(int id, CustomerUpdateModel customerPutModel, CancellationToken cancellationToken)
        {
            try
            {
                if (id <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                if (!id.Equals(customerPutModel.Id))
                {
                    throw new ServiceException(ServiceErrorType.DifferentIds);
                }

                var isCustomerExist = await _repository.CustomerRepository.GetAnyAsync(customer => customer.Id.Equals(id), cancellationToken);

                if (!isCustomerExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity); ;
                }

                var customer = _mapper.Map<Customer>(customerPutModel);
                var updatedCustomer = await _repository.CustomerRepository.UpdateAsync(customer);
                await _repository.SaveAsync(cancellationToken);

                return _mapper.Map<CustomerViewModel>(updatedCustomer);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }


        public async Task<bool> DeleteAsync(int customerId, CancellationToken cancellationToken)
        {
            try
            {
                if (customerId <= default(int))
                {
                    throw new ServiceException(ServiceErrorType.InvalidId);
                }

                var isCustomerExist =
                    await _repository.CustomerRepository.GetAnyAsync(customer => customer.Id.Equals(customerId),
                        cancellationToken);

                if (!isCustomerExist)
                {
                    throw new ServiceException(ServiceErrorType.NoEntity);
                    ;
                }

                var isCustomerDeleted = await _repository.CustomerRepository.DeleteAsync(customerId, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return isCustomerDeleted;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ServiceErrorType.UnknownException, "UnknownExeption", ex);
            }
        }
    }
}
