using AutoMapper;
using Basket.Application.Mapping;
using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Domain.Data.Entities;
using Basket.Infastructure.Repository.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TestsMapper.Core;

namespace TestsMapper
{
    public class RepositoryTests
    {
        private static IMapper _mapper;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = _serviceProvider.SetupServiceProvider();
            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Test]
        public async Task CreateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            //Act
            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(), new CancellationToken());
            await repositoryManager.SaveAsync(new CancellationToken());

            //Assert
            var customerView = await repositoryManager.CustomerRepository.GetByIdAsync(1, new CancellationToken());
            (_mapper.Map<CustomerViewModel>(customerView)).Should().BeEquivalentTo(TestHelper.CreateBaseViewModel());
        }

        [Test]
        public async Task GetAll_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            for (int i = 0; i < 10; i++)
            {
                await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            //Act
            var items = await repositoryManager.CustomerRepository.GetAllAsync(new CancellationToken());

            //Assert
            items.Count.Should().BeGreaterThanOrEqualTo(10);
        }

        [Test]
        public async Task GetAny_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            var baseEntity = TestHelper.CreateBaseCustomerEntity();

            await repositoryManager.CustomerRepository.CreateAsync(baseEntity,
                    new CancellationToken());
            await repositoryManager.SaveAsync(new CancellationToken());

            //Act
            var item = await repositoryManager.CustomerRepository.GetAnyAsync(x => x.Name.Equals(baseEntity.Name), new CancellationToken());

            //Assert
            Assert.True(item);
        }

        [Test]
        public async Task UpdateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            var baseEntity = TestHelper.CreateBaseCustomerEntity();

            await repositoryManager.CustomerRepository.CreateAsync(baseEntity,
                new CancellationToken());
            await repositoryManager.SaveAsync(new CancellationToken());

            baseEntity.Name = "changedName";

            var view = TestHelper.CreateBaseCustomerEntity();
            view.Id = 1;
            view.Name = "changedName";

            //Act
            await repositoryManager.CustomerRepository.UpdateAsync(baseEntity);
            await repositoryManager.SaveAsync(new CancellationToken());

            var result = await repositoryManager.CustomerRepository.GetByIdAsync(1, new CancellationToken());

            //Assert
            view.Should().BeEquivalentTo(result);

        }

        [Test]
        public async Task DeleteAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(), new CancellationToken());
            await repositoryManager.SaveAsync(new CancellationToken());

            //Act
            var isCustomerDeleted = await repositoryManager.CustomerRepository.DeleteAsync(1, new CancellationToken());

            //Assert
            Assert.IsTrue(isCustomerDeleted);
        }

        [Test]
        public async Task OrderRepository_CreateAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            for (int i = 0; i < 1; i++)
            {
                await repositoryManager.CustomerRepository.CreateAsync(TestHelper.CreateBaseCustomerEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            //Act
            var customerView = await repositoryManager.OrderRepository.GetByIdAsync(1, new CancellationToken(), nameof(Order.Customer), nameof(Order.Products));

            //Assert
            (_mapper.Map<OrderViewModel>(customerView)).Should().BeEquivalentTo(TestHelper.CreateBaseOrderViewModel());
        }

        [Test]
        public async Task GetListByIdsAsync_Shold_Be_Valid()
        {
            //Arrange
            var repositoryManager = _serviceProvider.GetService<IRepositoryManager>();

            for (int i = 1; i < 11; i++)
            {
                await repositoryManager.ProductRepository.CreateAsync(TestHelper.CreateBaseProductEntity(),
                    new CancellationToken());
            }

            await repositoryManager.SaveAsync(new CancellationToken());

            var ids = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            //Act
            var items = await repositoryManager.ProductRepository.GetListByIdsAsync(ids,
            new CancellationToken());

            //Assert
            items.Count.Should().BeGreaterThanOrEqualTo(10);
        }

        [TearDown]
        public void CleanUpOnError()
        {
            _serviceProvider.DeleteDatabase();
        }
    }
}
