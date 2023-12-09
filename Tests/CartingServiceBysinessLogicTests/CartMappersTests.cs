using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Mapers;
using CartingServiceDAL.Entities;

namespace DomainTests
{
    public class CartMappersTests
    {
        [Fact]
        public void ItemsToModelMapper_WhenMapListItems_ReturnsModel()
        {
            // Arrange
            List<CartItem> items = new List<CartItem>
            {
                new CartItem 
                { 
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "url",
                    Price = 20,
                    Quantity = 10
                },
                new CartItem
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "url2",
                    Price = 40,
                    Quantity = 20
                }
            };
            CartEntity entity = new()
            {
                Id = 1,
                Name = "Cart",
                Items = items
            };

            List<CartItemModel> itemsModel = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "url",
                    Price = 20,
                    Quantity = 10
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "url2",
                    Price = 40,
                    Quantity = 20
                }
            };

            CartModel expectedCartModel = new()
            {
                Id = 1,
                Name = "Cart",
                Items = itemsModel
            };

            // Act
            var result = ItemToEntityMappers.ItemToModelMapper().Map<List<CartItemModel>>(items);

            // Assert
            Assert.Equivalent(itemsModel, result);
        }

        [Fact]
        public void EntityToModelMapper_WhenMapEntity_ReturnsModel()
        {
            // Arrange
            List<CartItem> items = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "url",
                    Price = 20,
                    Quantity = 10
                },
                new CartItem
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "url2",
                    Price = 40,
                    Quantity = 20
                }
            };
            CartEntity entity = new()
            {
                Id = 1,
                Name = "Cart",
                Items = items
            };

            List<CartItemModel> itemsModel = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "url",
                    Price = 20,
                    Quantity = 10
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    ImageUrl = "url2",
                    Price = 40,
                    Quantity = 20
                }
            };

            CartModel expectedCartModel = new()
            {
                Id = 1,
                Name = "Cart",
                Items = itemsModel
            };

            // Act
            var result = ItemToEntityMappers.EntityToModelMapper().Map<CartModel>(entity);

            // Assert
            Assert.Equivalent(expectedCartModel, result);
        }
    }
}
