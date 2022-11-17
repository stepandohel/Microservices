using AutoMapper;
using Basket.Application.Middleware.Exceptions;
using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Application.Models.Product;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using Basket.Infastructure.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestsMapper.Core;

namespace TestsMapper
{
    public class ServiceTests
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider.SetupServiceProvider();
        }

        [Test]
        public async Task CustomerService_GetAllAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            for (int i = 0; i < 10; i++)
            {
                await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            var customerService = _serviceProvider.GetService<ICustomerService>();
            var resultCustomerView = TestHelper.CreateBaseViewModel();

            //Act
            var customersView = await customerService.GetAllAsync(new CancellationToken());

            //Assert
            foreach (var customer in customersView)
            {
                customer.Should().BeEquivalentTo(resultCustomerView);
                resultCustomerView.Id++;
            }
        }

        [Test]
        public async Task CustomerService_CreateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var customerService = _serviceProvider.GetService<ICustomerService>();

            var customerPostModel = new CustomerCreateModel()
            {
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };

            //Act
            await customerService.CreateAsync(customerPostModel, new CancellationToken());

            //Assert
            var result = await repositoryManager.CustomerRepository.GetByIdAsync(1, new CancellationToken());
            result.Should().BeEquivalentTo(TestHelper.CreateBaseViewModel());
        }

        //[Test]
        //public async Task CustomerService_UpdateAsync_Shold_Be_Valid()
        //{
        //    var repositoryManager = serviceProvider.GetService<IRepositoryManager>();
        //    var customerService = serviceProvider.GetService<ICustomerService>();
        //    var baseEntity = TestHelper.CreateBaseCustomerEntity();
        //    //await repositoryManager.CustomerRepository.CreateAsync(baseEntity,
        //    //    new CancellationToken());
        //    //await repositoryManager.SaveAsync(new CancellationToken());
        //    var customerPostModel = new CustomerPostModel()
        //    {
        //        Address = "random Street",
        //        Name = "Name",
        //        Surname = "Surname"
        //    };
        //    var putModel = new CustomerPutModel()
        //    {
        //        Id = 1,
        //        Address = "random Street",
        //        Name = "changedName",
        //        Surname = "Surname"
        //    };
        //    customerService.CreateAsync(customerPostModel, new CancellationToken()).Wait();
        //    var result = await customerService.UpdateAsync(1, putModel, new CancellationToken());

        //    var view = TestHelper.CreateBaseCustomerEntity();
        //    view.Id = 1;
        //    view.Name = "changedName";

        //    view.Should().BeEquivalentTo(result);
        //}

        [Test]
        public async Task CustomerService_DeleteAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var customerService = _serviceProvider.GetService<ICustomerService>();

            var customerPostModel = new CustomerCreateModel()
            {
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };

            await customerService.CreateAsync(customerPostModel, new CancellationToken());

            //Act
            var result = await customerService.DeleteAsync(1, new CancellationToken());

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ProductService_GetAllAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            for (int i = 0; i < 10; i++)
            {
                await repositoryManager.ProductRepository.CreateAsync(TestHelper.CreateBaseProductEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            var productService = _serviceProvider.GetService<IProductService>();
            var resultCustomerView = TestHelper.CreateBaseProductViewModel();

            //Act
            var productViews = await productService.GetAllAsync(new CancellationToken());

            //Assert
            foreach (var product in productViews)
            {
                product.Should().BeEquivalentTo(resultCustomerView);
                resultCustomerView.Id++;
            }
        }

        [Test]
        public async Task ProductService_CreateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var productService = _serviceProvider.GetService<IProductService>();

            var productPostModel = new ProductPostModel()
            {
                Price = 20
            };

            //Act
            await productService.CreateAsync(productPostModel, new CancellationToken());

            //Assert
            var result = await repositoryManager.ProductRepository.GetByIdAsync(1, new CancellationToken());
            result.Should().BeEquivalentTo(TestHelper.CreateBaseProductViewModel());
        }

        [Test]
        public async Task ProductService_DeleteAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var productService = _serviceProvider.GetService<IProductService>();

            var productPostModel = new ProductPostModel()
            {
                Price = 20
            };

            await productService.CreateAsync(productPostModel, new CancellationToken());

            //Act
            var result = await productService.DeleteAsync(1, new CancellationToken());

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ProductService_GetListByIdsAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var productService = _serviceProvider.GetService<IProductService>();

            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                new CancellationToken());

            for (int i = 0; i < 1; i++)
                await repositoryManager.ProductRepository.CreateAsync(TestHelper.CreateBaseProductEntity(),
                    new CancellationToken());

            var productView = TestHelper.CreateBaseProductEntity();

            var ids = new List<int>()
            {
                1,
                2
            };

            //Act
            var result = await productService.GetListByIdsAsync(ids, new CancellationToken());

            //Assert
            foreach (var product in result)
            {
                product.Should().BeEquivalentTo(productView);
                productView.Id++;
            }
        }

        [Test]
        public async Task OrderService_GetAllAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>()
                ;
            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                new CancellationToken());

            for (int i = 0; i < 1; i++)
            {
                await repositoryManager.OrderRepository.CreateAsync(TestHelper.CreateBaseOrderEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            var orderService = _serviceProvider.GetService<IOrderService>();
            var resultCustomerView = TestHelper.CreateBaseOrderViewModel();

            //Act
            var orderViews = await orderService.GetAllAsync(new CancellationToken());

            //Assert
            foreach (var product in orderViews)
            {
                product.Should().BeEquivalentTo(resultCustomerView);
                resultCustomerView.Id++;
            }
        }

        [Test]
        public async Task OrderService_CreateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var orderService = _serviceProvider.GetService<IOrderService>();
            var mapper = _serviceProvider.GetService<IMapper>();

            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                new CancellationToken());
            await repositoryManager.ProductRepository.CreateAsync(TestHelper.CreateBaseProductEntity(),
                new CancellationToken());

            var secondProduct = TestHelper.CreateBaseProductEntity();
            secondProduct.Price = 30;

            await repositoryManager.ProductRepository.CreateAsync(secondProduct,
                new CancellationToken());
            await repositoryManager.SaveAsync(new CancellationToken());

            var orderPostModel = new OrderPostModel()
            {
                CustomerId = 1,
                Products = new List<int>
                {
                    1,
                    2
                }
            };

            //Act
            await orderService.CreateAsync(orderPostModel, new CancellationToken());

            //Assert
            var result = await repositoryManager.OrderRepository.GetByIdAsync(1, new CancellationToken(), nameof(Order.Customer), nameof(Order.Products));
            (mapper.Map<OrderViewModel>(result)).Should().BeEquivalentTo(TestHelper.CreateBaseOrderViewModel());
        }

        [Test]
        public async Task OrderService_DeleteAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();
            var orderService = _serviceProvider.GetService<IOrderService>();

            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                new CancellationToken());
            await repositoryManager.ProductRepository.CreateAsync(TestHelper.CreateBaseProductEntity(),
                new CancellationToken());

            var orderPostModel = new OrderPostModel()
            {
                CustomerId = 1,
                Products = new List<int>
                {
                    1
                }
            };

            await orderService.CreateAsync(orderPostModel, new CancellationToken());

            //Act
            var result = await orderService.DeleteAsync(1, new CancellationToken());

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task OrderService_DeleteAsync_Shold_Be_Invalid()
        {
            //Arrange
            var orderService = _serviceProvider.GetService<IOrderService>();

            //Act&Assert
            await orderService.Invoking(x => x.DeleteAsync(2, new CancellationToken())).Should().ThrowAsync<ServiceException>();

        }

        [TearDown]
        public void CleanUpOnError()
        {
            _serviceProvider.DeleteDatabase();
        }
    }
}
