using LiteDB;
using CartingServiceDAL.Entities;
using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;

namespace CartingServiceBysinessLogicTests
{
    public class CartBusinessLogicTests
    {
        private readonly string _testDatabaseName = "TestDatabase.db";
        private readonly string _testCollectionName = "TestCartName";

        public CartBusinessLogicTests()
        {
            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.DeleteAll();
            }
        }

        [Fact]
        public void Add_WhenEntitiIsCorrect_ReturnsId()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var cartItem = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            var id = cartActions.AddToCart(cartItem).Result;

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public void RemoevFromCart_WhenEntitiIsCorrect_ReturnsTrue()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var cartItem = new CartItem()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(cartItem);
            }

            // Act
            var result = cartActions.RemoevFromCart(cartItem).Result;

            // Assert
            Assert.True(result);
            var resultItem = new CartItem();
            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                resultItem = collection.Find(e => e.Id == cartItem.Id).FirstOrDefault();
            }
            Assert.True(resultItem == null);
        }

        [Fact]
        public void GetListItems_WhenEntitiIsCorrect_ReturnsListItems()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var expectedItems = new List<CartItem>
            {
                new CartItem
                {
                    Name = "TestItemOne",
                    Price = 10,
                    Quantity = 1
                },
                new CartItem
                {
                    Name = "TestItemTwo",
                    Price = 20,
                    Quantity = 2
                }
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(expectedItems);
            }

            // Act
            var result = cartActions.GetListItems().Result;

            // Assert
            Assert.True(result.Count == 2);
            Assert.Equivalent(expectedItems, result);
        }

        [Fact]
        public void AddItemToCart_WhenEntitiIsCorrect_ReturnsCartWithItems()
        {
            // Arrange
            var cart = new CartEntity(_testDatabaseName, _testCollectionName);

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
            var cart = new CartEntity(_testDatabaseName, _testCollectionName);
            cart.CartName = _testCollectionName;

            // Act
            var cartName = cart.CartName;

            // Assert
            Assert.Equivalent(cartName, _testCollectionName);
        }

        [Fact]
        public void GetItems_WhenEntitiIsCorrect_ReturnsCartItems()
        {
            // Arrange
            var cart = new CartEntity(_testDatabaseName, _testCollectionName);

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