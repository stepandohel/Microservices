using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Grpc.Core;
using GrpcService;

namespace Basket.Infastructure.GRPC.GrpcServices
{
    public class CustomerGrpcService : Greeter.GreeterBase
    {
        private readonly IRepositoryManager _repository;
        public CustomerGrpcService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public override async Task<ReplyModel> GetCustomers(Request request, ServerCallContext context)
        {
            var customers = await _repository.CustomerRepository.GetAllAsync(new CancellationToken());
           
            List<CustomerGrpc> customersGrpc = new List<CustomerGrpc>();
            foreach (var item in customers)
            {
                customersGrpc.Add(new CustomerGrpc
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Adress = item.Address
                });
            }
            
            ReplyModel replyModel = new ReplyModel();
            replyModel.Customergrpc.AddRange(customersGrpc);
            
            return replyModel;
        }
        public override async Task<ReplyPostModel> PostCustomer(PostRequest request, ServerCallContext context)
        {
            var customer = new Customer();
            customer.Name = request.Name;
            customer.Surname = request.Surname;
            customer.Address = request.Adress;
            
            var id = await _repository.CustomerRepository.CreateAsync(customer, new CancellationToken());
            await _repository.SaveAsync();

            ReplyPostModel replyModel = new ReplyPostModel();
            replyModel.Id = id;
            
            return replyModel;
        }
    }
}
