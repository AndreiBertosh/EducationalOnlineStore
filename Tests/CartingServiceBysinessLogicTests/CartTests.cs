using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceBysinessLogicTests
{
    public class CartTests
    {
        private readonly string _testDatabaseName = "TestCartDatabase.db";
        private readonly string _testCartName = "TestCartName";

        public CartTests()
        {
            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCartName);
                collection.DeleteAll();
            }
        }

        [Fact]
        public void AddItemToCart_WhenEntitiIsCorrect_ReturnsCartWithItems()
        {
            // Arrange
            var cart = new CartEntity(_testDatabaseName, _testCartName);

            var existingCartItem = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            cart.Actions.AddToCart(existingCartItem);

            // Assert
            var resultItem = cart.Items.Where(x => x.Id == existingCartItem.Id).FirstOrDefault();

            Assert.Equivalent(resultItem, existingCartItem);
        }

        [Fact]
        public void CartName_WhenCartInitialised_ReturnsCartName()
        {
            // Arrange
            var cart = new CartEntity(_testDatabaseName, _testCartName);
            cart.CartName = _testCartName;

            // Act
            var cartName = cart.CartName;

            // Assert
            Assert.Equivalent(cartName, _testCartName);
        }

        [Fact]
        public void GetItems_WhenEntitiIsCorrect_ReturnsCartItems()
        {
            // Arrange
            var cart = new CartEntity(_testDatabaseName, _testCartName);

            var existingCartItems = new List<CartItem> {
                new CartItem()
                {
                    Name = "TestItem",
                    Price = 10,
                    Quantity = 1
                },
                new CartItem()
                {
                    Name = "TestItem2",
                    Price = 15,
                    Quantity = 1
                },
            };
            existingCartItems.ForEach(item => cart.Actions.AddToCart(item));

            // Act
            var resultCartItems = cart.Items;

            // Assert
            Assert.Equivalent(resultCartItems, existingCartItems);
        }
    }
}
