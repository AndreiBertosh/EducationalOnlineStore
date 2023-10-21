using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;
using LiteDB;

namespace CartingServiceDALTests
{
    public class RepositoryTests
    {
        private readonly string _testDatabaseName = "TestDatabase.db";
        private readonly string _testCollectionName = "TestCollectionName";

        public RepositoryTests()
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
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

            var chartItem = new CartItem()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            var id = repository.Add(chartItem).Result;

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public void Delete_WhenEntitiIsCorrect_ReturnsTrue()
        {
            // Arrange
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

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
            var result = repository.Delete(chartItem).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAll_WhenEntitiIsCorrect_ReturnsListChartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

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
            var result = repository.GetAll().Result;

            // Assert
            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void GetById_WhenEntitiIsCorrect_ReturnsChartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItem()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(expectedItem);
            }

            // Act
            var result = repository.GetById(expectedItem.Id).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(expectedItem, result);
        }

        [Fact]
        public void GetById_WhenEntitiIsInCorrect_ReturnsNull()
        {
            // Arrange
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItem()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(expectedItem);
            }

            // Act
            var result = repository.GetById(expectedItem.Id + 5).Result;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_WhenEntitiIsCorrect_ReturnsChartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItem>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItem()
            {
                Name = "TestItemToUpdate",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItem>(_testCollectionName);
                collection.Insert(expectedItem);
            }

            expectedItem.Name = "NameUpdated";

            // Act
            var result = repository.Update(expectedItem).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedItem.Name, result.Name);
            Assert.Equivalent(expectedItem, result);
        }
    }
}