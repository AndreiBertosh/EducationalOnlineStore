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
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
                collection.DeleteAll();
            }
        }

        [Fact]
        public void Add_WhenEntitiIsCorrect_ReturnsId()
        {
            // Arrange
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var cartItem = new CartItemModel()
            {
                Name = "TestItem",
                Price = 10,
                Quantity = 1
            };

            // Act
            int id = repository.Add(cartItem).Result;

            // Assert
            Assert.True(id > 0);
        }

        [Fact]
        public void Delete_WhenEntitiIsCorrect_ReturnsTrue()
        {
            // Arrange
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var cartItem = new CartItemModel()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
                collection.Insert(cartItem);
            }

            // Act
            bool result = repository.Delete(cartItem).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAll_WhenEntitiIsCorrect_ReturnsListCartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var expectedItems = new List<CartItemModel>
            {
                new CartItemModel
                {
                    Name = "TestItemOne",
                    Price = 10,
                    Quantity = 1
                },
                new CartItemModel
                {
                    Name = "TestItemTwo",
                    Price = 20,
                    Quantity = 2
                }
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
                collection.Insert(expectedItems);
            }

            // Act
            var result = repository.GetAll().Result;

            // Assert
            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void GetById_WhenEntitiIsCorrect_ReturnsCartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItemModel()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
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
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItemModel()
            {
                Name = "TestItemToDelete",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
                collection.Insert(expectedItem);
            }

            // Act
            var result = repository.GetById(expectedItem.Id + 5).Result;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_WhenEntitiIsCorrect_ReturnsCartItem()
        {
            // Arrange
            var repository = new CartRepository<CartItemModel>(_testDatabaseName, _testCollectionName);

            var expectedItem = new CartItemModel()
            {
                Name = "TestItemToUpdate",
                Price = 10,
                Quantity = 1
            };

            using (var database = new LiteDatabase(_testDatabaseName))
            {
                var collection = database.GetCollection<CartItemModel>(_testCollectionName);
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