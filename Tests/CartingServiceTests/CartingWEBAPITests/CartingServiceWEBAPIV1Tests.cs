using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceDAL.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Controllers.V1;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingWEBAPITests
{
    public class CartingServiceWEBAPIV1Tests
    {
        private readonly Mock<ICartActions<CartItem>> _mockService;
        private CartingServiceController _cartingServiceControllerV1;
        private readonly Mock<ILogger<CartingServiceController>> _logger;
        private readonly Mock<ICartProvider> _provider;

        public CartingServiceWEBAPIV1Tests()
        { 
            _logger = new Mock<ILogger<CartingServiceController>>();
            _provider = new Mock<ICartProvider>();
            _mockService = new Mock<ICartActions<CartItem>>();
        }

        [Fact]
        public async Task GetCart_ReturnsOkResult()
        {
            // Arrange
            CartItem cartEntity = new()
            {
                Id = 1,
                Name = "Entity Name",
                Image = "url",
                Price = 10,
                Quantity = 10
            };
            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(new List<CartItem> { cartEntity });

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.GetCart();
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value as Cart;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
        }

        [Fact]
        public async Task Add_ReturnsOkResult()
        {
            // Arrange
            CartItem cartEntity = new()
            {
                Id = 1,
                Name = "Entity Name",
                Image = "url",
                Price = 10,
                Quantity = 10
            };

            var returnList = new List<CartItem> { cartEntity };

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);
            cart.Setup(c => c.AddToItems(It.IsAny<CartItem>())).Returns(1);

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Add(cartEntity);
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value ;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
        }

        [Fact]
        public async Task Add_ReturnsBadRequestResult()
        {
            // Arrange
            CartItem cartEntity = new()
            {
            };

            var returnList = new List<CartItem> { cartEntity };

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);
            cart.Setup(c => c.AddToItems(It.IsAny<CartItem>())).Returns(0);

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Add(cartEntity);
            var resultType = result as BadRequestObjectResult;
            var resultValue = resultType?.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(400, resultType.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            CartItem cartEntity = new()
            {
                Id = 1,
                Name = "Entity Name",
                Image = "url",
                Price = 10,
                Quantity = 10
            };

            var returnList = new List<CartItem> { cartEntity };

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);
            cart.Setup(c => c.RemoveItem(It.IsAny<CartItem>())).Returns(true);

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Delete(1);
            var resultType = result as OkObjectResult;
            var resultValue = resultType?.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(200, resultType.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequestResult()
        {
            // Arrange
            CartItem cartEntity = new()
            {
                Id = 5,
                Name = "Entity Name",
                Image = "url",
                Price = 10,
                Quantity = 10
            };

            var returnList = new List<CartItem> { cartEntity };

            Mock<ICart> cart = new Mock<ICart>();
            cart.Setup(c => c.CartName).Returns("Cart name");
            cart.Setup(c => c.Items).Returns(returnList);
            cart.Setup(c => c.RemoveItem(It.IsAny<CartItem>())).Returns(false);

            _provider.Setup(p => p.Cart).Returns(cart.Object);
            _cartingServiceControllerV1 = new CartingServiceController(_logger.Object, _provider.Object);

            // Act
            var result = _cartingServiceControllerV1.Delete(1);
            var resultType = result as BadRequestObjectResult;
            var resultValue = resultType?.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(400, resultType.StatusCode);
        }
    }
}
