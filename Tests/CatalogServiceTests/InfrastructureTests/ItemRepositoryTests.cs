using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTests
{
    public class ItemRepositoryTests : IDisposable
    {
        private readonly string connection = @"data source=(localdb)\MSSQLLocalDB;Initial Catalog=TestCatalogDb;Integrated Security=True;";
        private readonly InfrastructureContext testDatabase;


        public ItemRepositoryTests()
        {
            InfrastructureContext db = new()
            {
                Connection = connection
            };

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            testDatabase = db;
        }

        public void Dispose()
        {
            testDatabase.Categories.RemoveRange(testDatabase.Categories);
            testDatabase.SaveChanges();
            testDatabase.Dispose();
        }

        [Fact]
        public void AddItem_WhenModelIsOk_ReturnsId()
        {
            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();

            ItemModel item = new() { Name = "testItem", CategoryId = category.Id };
            var repository = new ItemRepository(testDatabase);

            // Act
            repository.Add(item);

            // Assert
            Assert.True(testDatabase.Items.Any());
            Assert.True(item.Id > 0);
        }

        [Fact]
        public void DeleteItem_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange

            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            ItemModel item = new() { Name = "testItem", CategoryId = category.Id };
            testDatabase.Items.Add(item);
            testDatabase.SaveChanges();

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.Delete(item.Id).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateItem_WhenModelIsOk_ReturnsTrueAndModelIsChanged()
        {
            // Arrange

            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            ItemModel item = new() { Name = "testItem", CategoryId = category.Id };
            testDatabase.Items.Add(item);
            testDatabase.SaveChanges();

            item.Name = "New Name";
            item.Description = "Description";

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.Update(item).Result;

            // Assert
            Assert.True(result);

            var updatedItem = testDatabase.Items.Find(item.Id);
            Assert.Equivalent(updatedItem?.Name, "New Name");
            Assert.Equivalent(updatedItem?.Description, "Description");
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsItemModel()
        {

            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            ItemModel item = new() { Name = "testItem", CategoryId = category.Id, Description = "Description" };
            testDatabase.Items.Add(item);
            testDatabase.SaveChanges();

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.GetById(item.Id).Result;

            // Assert
            Assert.Equivalent(item, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListItemModel()
        {
            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            List<ItemModel> expectrdItemModels = new()
            {
                new() { Name = "testitem1", CategoryId = category.Id, Description = "Description" },
                new() { Name = "testitem2", CategoryId = category.Id, Description = "Description" }
            };

            testDatabase.Items.AddRange(expectrdItemModels);
            testDatabase.SaveChanges();

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            var intersected = result.Intersect(expectrdItemModels).ToList();
            Assert.Equivalent(expectrdItemModels, intersected);
        }
    }
}