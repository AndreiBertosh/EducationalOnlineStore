using Domain.Entities;
using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
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
            testDatabase.Items.RemoveRange(testDatabase.Items);
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

            var expectedRecordsCount = testDatabase.Items.Count() + 1;

            Item item = new() { Name = "testItem", CategoryId = category.Id };
            var repository = new ItemRepository(testDatabase);

            // Act
            var id = repository.Add(item).Result;

            // Assert
            Assert.True(testDatabase.Items.Any());
            Assert.True(id > 0);

            var recordsCount = testDatabase.Items.Count();
            Assert.Equal(expectedRecordsCount, recordsCount);
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
        public void DeleteCategory_WhenRecordNotFound_ReturnsFalse()
        {
            // Arrange
            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.Delete(1000000).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateItem_WhenModelIsOk_ReturnsTrueAndModelIsChanged()
        {
            // Arrange

            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            ItemModel itemModel = new()
            { 
                Name = "testItem", 
                CategoryId = category.Id 
            };
            testDatabase.Items.Add(itemModel);
            testDatabase.SaveChanges();

            Item item = new()
            {
                Id = itemModel.Id,
                Name = "New Name",
                CategoryId = category.Id,
                Description = "Description"
            };

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.Update(item).Result;

            // Assert
            Assert.True(result);

            var updatedItem = testDatabase.Items.Find(itemModel.Id);
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
            ItemModel itemModel = new() 
            { 
                Name = "testItem", 
                CategoryId = category.Id, 
                Description = "Description" 
            };
            testDatabase.Items.Add(itemModel);
            testDatabase.SaveChanges();

            Item expectedItem = EntityModelMappers.ModelToItemMapper().Map<Item>(itemModel);

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.GetById(itemModel.Id).Result;

            // Assert
            Assert.Equivalent(expectedItem, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListItemModel()
        {
            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            List<ItemModel> itemModels = new()
            {
                new() 
                { 
                    Name = "testitem1", 
                    CategoryId = category.Id, 
                    Description = "Description" 
                },
                new()
                {
                    Name = "testitem2",
                    CategoryId = category.Id,
                    Description = "Description" 
                }
            };

            testDatabase.Items.AddRange(itemModels);
            testDatabase.SaveChanges();

            List<Item> expectedItems = EntityModelMappers.ModelToItemMapper().Map<List<Item>>(itemModels);

            var repository = new ItemRepository(testDatabase);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            foreach (Item item in expectedItems)
            {
                Assert.True(result.Any(c => c.Id == item.Id && c.Name == item.Name));
            }
        }
    }
}