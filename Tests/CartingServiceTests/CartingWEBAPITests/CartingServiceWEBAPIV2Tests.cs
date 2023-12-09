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
        private readonly Mock<ICartActions<CartItem>> _mockService;
        private CartingServiceController _cartingServiceControllerV2;
        private readonly Mock<ILogger<CartingServiceController>> _logger;
        private readonly Mock<ICartProvider> _provider;

        public CartingServiceWEBAPIV2Tests()
        { 
            _logger = new Mock<ILogger<CartingServiceController>>();
            _provider = new Mock<ICartProvider>();
            _mockService = new Mock<ICartActions<CartItem>>();
        }

        [Fact]
        public async Task GetCart_ReturnsOkResult()
        {
            // Arrange
            CartItem cartItem = new()
            {
                Id = 5,
                Name = "Entity Name",
                ImageUrl = "url",
                Price = 10,
                Quantity = 10
            };

            CartEntity cartEntity = new()
            {
                Id= 1,
                Name = "CartName",
                Items = new List<CartItem> { cartItem }
            };

            var returnList = new List<CartItem> { cartItem };
            Mock<ICartActionsNew<CartEntity>> cartActions = new Mock<ICartActionsNew<CartEntity>>();
            cartActions.Setup(c => c.GetCart(It.IsAny<string>())).Returns(Task.FromResult(cartEntity));
            _provider.Setup(p => p.CartActions).Returns(cartActions.Object);

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);

            _provider.Setup(p => p.Cart).Returns(cart.Object);

            _cartingServiceControllerV2 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV2.GetCart("CartName");
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value as CartEntity;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(resultValue, cartEntity);
            Assert.Equivalent(200, resultType.StatusCode);
        }
    }
}
