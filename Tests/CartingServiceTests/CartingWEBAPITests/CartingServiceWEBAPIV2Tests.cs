using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Controllers.V2;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CartingWEBAPITests
{
    public class CartingServiceWEBAPIV2Tests
    {
        private readonly Mock<ICartActions<CartEntity>> _mockService;
        private CartingServiceController _cartingServiceControllerV2;
        private readonly Mock<ILogger<CartingServiceController>> _logger;
        private readonly Mock<ICartProvider> _provider;

        public CartingServiceWEBAPIV2Tests()
        { 
            _logger = new Mock<ILogger<CartingServiceController>>();
            _provider = new Mock<ICartProvider>();
            _mockService = new Mock<ICartActions<CartEntity>>();
        }

        [Fact]
        public async Task GetCart_ReturnsOkResult()
        {
            // Arrange
            CartEntity cartEntity = new()
            {
                Id = 5,
                Name = "Entity Name",
                Image = "url",
                Price = 10,
                Quantity = 10
            };

            var returnList = new List<CartEntity> { cartEntity };

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV2 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV2.GetCart("CartName");
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value as Cart;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
        }
    }
}
