using AutoMapper;
using Basket.Application.Mapping;
using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Application.Models.Product;
using Basket.Domain.Data.Entities;
using FluentAssertions;

namespace TestsMapper
{
    public class Tests
    {
        private static IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            _mapper = mapper;
        }

        [Test]
        public void All_Mappings_Should_Be_Valid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

    }
}