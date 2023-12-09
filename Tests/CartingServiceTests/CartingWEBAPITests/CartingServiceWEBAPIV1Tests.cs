using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Controllers.V1;
using CartingServiceWEBAPI.Infrastructure.Requests;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartingWEBAPITests
{
    public class CartingServiceWEBAPIV1Tests
    {
        private readonly Mock<ICartActionsNew<CartEntity>> _mockActions;
        private CartingServiceController _cartingServiceControllerV1;
        private readonly Mock<ILogger<CartingServiceController>> _logger;
        private readonly Mock<ICartProvider> _provider;

        public CartingServiceWEBAPIV1Tests()
        { 
            _logger = new Mock<ILogger<CartingServiceController>>();
            _provider = new Mock<ICartProvider>();
            _mockActions = new Mock<ICartActionsNew<CartEntity>>();
        }

        [Fact]
        public async Task GetCart_ReturnsOkResult()
        {
            // Arrange
            List<CartItem> cartItems = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Entity Name",
                    ImageUrl = "url",
                    Price = 10,
                    Quantity = 10
                }
            };

            CartEntity cartEntity = new()
            {
                Id = 1,
                Name = "Cart name",
                Items = cartItems
            };

            _mockActions.Setup(c => c.GetCart(It.IsAny<string>())).Returns(Task.FromResult(cartEntity));

            _provider.Setup(p => p.CartActions).Returns(_mockActions.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.GetCart("Cart name");
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value as List<CartItem>;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
            Assert.Equivalent(cartItems, resultValue);
        }

        [Fact]
        public async Task Add_ReturnsOkResult()
        {
            // Arrange
            List<CartItem> cartItems = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Entity Name",
                    ImageUrl = "url",
                    Price = 10,
                    Quantity = 10
                }
            };

            CartEntity cartEntity = new()
            {
                Id = 1,
                Name = "Cart name",
                Items = cartItems
            };

            _mockActions.Setup(c => c.AddToCart(It.IsAny<CartEntity>())).Returns(Task.FromResult(1));

            _provider.Setup(p => p.CartActions).Returns(_mockActions.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Add(cartEntity);
            var resultType = result as OkObjectResult;
            var resultValue = Convert.ToInt32(resultType?.Value);

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
            Assert.Equivalent(1, resultValue);
        }

        [Fact]
        public async Task Add_ReturnsBadRequestResult()
        {
           // Arrange
            List<CartItem> cartItems = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Entity Name",
                    ImageUrl = "url",
                    Price = 10,
                    Quantity = 10
                }
            };

            CartEntity cartEntity = new()
            {
                Id = 1,
                Name = "Cart name",
                Items = cartItems
            };

            _mockActions.Setup(c => c.AddToCart(It.IsAny<CartEntity>())).Returns(Task.FromResult(0));

            _provider.Setup(p => p.CartActions).Returns(_mockActions.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Add(cartEntity);
            var resultType = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(400, resultType.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            DeleteRequest request = new()
            {
                cartItemId = 1,
                Name = "Entity Name"
            };
            _mockActions.Setup(c => c.RemoevFromCart(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(true));

            _provider.Setup(p => p.CartActions).Returns(_mockActions.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Delete(request);
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequestResult()
        {
            DeleteRequest request = new()
            {
                cartItemId = 0,
                Name = "Entity Name"
            };
            _mockActions.Setup(c => c.RemoevFromCart(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(false));

            _provider.Setup(p => p.CartActions).Returns(_mockActions.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Delete(request);
            var resultType = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(400, resultType.StatusCode);
        }
    }
}
