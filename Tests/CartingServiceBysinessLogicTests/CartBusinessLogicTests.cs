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
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
                collection.DeleteAll();
            }
        }

        [Fact]
        public void Add_WhenEntitiIsCorrect_ReturnsId()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var cartEntity = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            int id = cartActions.AddToCart(cartEntity).Result;

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public void RemoevFromCart_WhenEntitiIsCorrect_ReturnsTrue()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var cartEntity = new CartItem()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(cartEntity);
            }

            // Act
            bool result = cartActions.RemoevFromCart(cartEntity).Result;

            // Assert
            Assert.True(result);
            var resultItem = new CartItem();
            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                resultItem = collection.Find(e => e.Id == cartEntity.Id).FirstOrDefault();
            }
            Assert.True(resultItem == null);
        }

        [Fact]
        public void GetListItems_WhenEntitiIsCorrect_ReturnsListItems()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var expectedEntities = new List<CartItem>
            {
                new ()
                {
                    Name = "TestItemOne",
                    Price = 10,
                    Quantity = 1
                },
                new ()
                {
                    Name = "TestItemTwo",
                    Price = 20,
                    Quantity = 2
                }
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(expectedEntities);
            }

            // Act
            var result = cartActions.GetListItems().Result;

            // Assert
            Assert.True(result.Count == 2);
            Assert.Equivalent(expectedEntities, result);
        }
    }
}