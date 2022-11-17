using Basket.Application.Models.Customer;
using Basket.Application.Models.Order;
using Basket.Application.Models.Product;
using Basket.Application.Validators;
using FluentAssertions;

namespace TestsMapper
{
    public class ValidatorsTest
    {
        private CustomerPostValidator customerPostValidator;
        private CustomerPutValidator customerPutValidator;
        private OrderPostValidator orderPostValidator;
        private OrderPutValidator orderPutValidator;
        private ProductPostValidator productPostValidator;
        private ProductPutValidator productPutValidator;

        [SetUp]
        public void Setup()
        {
            customerPostValidator = new CustomerPostValidator();
            customerPutValidator = new CustomerPutValidator();
            orderPostValidator = new OrderPostValidator();
            orderPutValidator = new OrderPutValidator();
            productPostValidator = new ProductPostValidator();
            productPutValidator = new ProductPutValidator();
        }

        [Test]
        public async Task CustomerPostValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new CustomerCreateModel()
            {
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };

            //Act&Assert
            customerPostValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task CustomerPostValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new CustomerCreateModel();

            //Act
            var result = customerPostValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "Address"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Name"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Surname"));
        }

        [Test]
        public async Task CustomerPutValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new CustomerUpdateModel()
            {
                Id = 1,
                Address = "random Street",
                Name = "Name",
                Surname = "Surname"
            };

            //Act&Assert
            customerPutValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task CustomerPutValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new CustomerUpdateModel();

            //Act
            var result = customerPutValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "Address"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Name"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Surname"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Id"));
        }

        [Test]
        public async Task ProductPostValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new ProductPostModel()
            {
                Price = 10
            };

            //Act&Assert
            productPostValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task ProductPostValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new ProductPostModel();

            //Act
            var result = productPostValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "Price"));
        }

        [Test]
        public async Task ProductPutValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new ProductPutModel()
            {
                Id = 1,
                Price = 10
            };

            //Act&Assert
            productPutValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task ProductPutValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new ProductPutModel();

            //Act
            var result = productPutValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "Price"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Id"));
        }

        [Test]
        public async Task OrderPostValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new OrderPostModel()
            {
                CustomerId = 1,
                Products = new List<int>
                {
                    1,
                    2
                }
            };

            //Act&Assert
            orderPostValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task OrderPostValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new OrderPostModel();

            //Act
            var result = orderPostValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "CustomerId"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Products"));
        }

        [Test]
        public async Task OrderPutValidator_Shold_Be_Valid()
        {
            //Arrange
            var model = new OrderPutModel()
            {
                Id = 1,
                CustomerId = 1,
                Products = new List<int>
                {
                    1,
                    2
                }
            };

            //Act&Assert
            orderPutValidator.Validate(model).IsValid.Should().BeTrue();
        }

        [Test]
        public async Task OrderPutValidator_Shold_Be_Invalid()
        {
            //Arrange
            var model = new OrderPutModel();

            //Act
            var result = orderPutValidator.Validate(model);

            //Assert
            Assert.That(result.Errors.Any(x => x.PropertyName == "CustomerId"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Products"));
            Assert.That(result.Errors.Any(x => x.PropertyName == "Id"));
        }
    }
}
