using LiteDB;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;
using CartingService;

namespace CartingServiceBysinessLogicTests
{
    public class CartBusinessLogicTests
    {
        private readonly string _testDatabaseName = "TestDatabase.db";
        private readonly string _testCollectionName = "TestCollectionName";

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

            var chartItem = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            var id = cartActions.AddToChart(chartItem).Result;

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public void RemoevFromChart_WhenEntitiIsCorrect_ReturnsTrue()
        {
            // Arrange
            var cartActions = new CartActions<CartItem>(_testDatabaseName, _testCollectionName);

            var chartItem = new CartItem()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(chartItem);
            }

            // Act
            var result = cartActions.RemoevFromChart(chartItem).Result;

            // Assert
            Assert.True(result);
            var resultItem = new CartItem();
            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                resultItem = collection.Find(e => e.Id == chartItem.Id).FirstOrDefault();
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
    }
}