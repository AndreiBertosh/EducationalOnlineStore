using CartingService;
using CartingServiceBusinessLogic;
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
            var cart = new CartService(_testDatabaseName, _testCartName);

            var existingCartItem = new CartEntity()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            cart.CartActions.AddToCart(existingCartItem);

            // Assert
            var resultItem = cart.CartActions.GetListItems().Result.Where(x => x.Id == existingCartItem.Id).FirstOrDefault();

            Assert.Equivalent(resultItem, existingCartItem);
        }

        [Fact]
        public void GetItems_WhenEntitiIsCorrect_ReturnsCartItems()
        {
            // Arrange
            var cart = new CartService(_testDatabaseName, _testCartName);

            var existingCartItems = new List<CartEntity> {
                new ()
                {
                    Name = "TestItem",
                    Price = 10,
                    Quantity = 1
                },
                new ()
                {
                    Name = "TestItem2",
                    Price = 15,
                    Quantity = 1
                },
            };
            existingCartItems.ForEach(item => cart.CartActions.AddToCart(item));

            // Act
            var resultCartItems = cart.CartActions.GetListItems().Result;

            // Assert
            Assert.Equivalent(resultCartItems, existingCartItems);
        }
    }
}
