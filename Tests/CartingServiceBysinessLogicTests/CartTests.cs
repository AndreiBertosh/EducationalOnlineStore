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
                var collection = database.GetCollection<CartItemModel>(_testCartName);
                collection.DeleteAll();
            }
        }

        [Fact]
        public void AddItemToCart_WhenEntitiIsCorrect_ReturnsCartWithItems()
        {
            // Arrange
            var cart = new CartService(_testDatabaseName, _testCartName);

            var existingCartEntity = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            int id = cart.CartActions.AddToCart(existingCartEntity).Result;

            // Assert
            var resultItem = cart.CartActions.GetListItems().Result.FirstOrDefault(x => x.Id == id);
            existingCartEntity.Id = id;

            Assert.Equivalent(resultItem, existingCartEntity);
        }

        [Fact]
        public void GetItems_WhenEntitiIsCorrect_ReturnsCartItems()
        {
            // Arrange
            var cart = new CartService(_testDatabaseName, _testCartName);

            var existingCartEntities = new List<CartItem> {
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
            existingCartEntities.ForEach(item => item.Id = cart.CartActions.AddToCart(item).Result);

            // Act
            var resultCartEntities = cart.CartActions.GetListItems().Result;

            // Assert
            Assert.Equivalent(resultCartEntities, existingCartEntities);
        }
    }
}
