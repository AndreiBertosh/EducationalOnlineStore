using Application;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTests
{
    public class CatalogServiceTests : IDisposable
    {
        private readonly string _connection = @"data source=(localdb)\MSSQLLocalDB;Initial Catalog=TestCatalogDb;Integrated Security=True;";
        private readonly InfrastructureContext testDatabase;
        private CatalogService _service;

        public CatalogServiceTests()
        {
            _service = new(_connection);

            InfrastructureContext db = new()
            {
                Connection = _connection
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
        public void CreateCategory_WhenConnectionStringIsOk_ReturnsService()
        {
            // Arrange
            Category category = new()
            {
                Name = "Category Name"
            };

            // Act
            var result = _service.CategoryActions.Add(category).Result;

            // Assert
            Assert.True(result > 0);
        }

        [Fact]
        public void DeleteCategory_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange
            CategoryModel category = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();

            // Act
            var result = _service.CategoryActions.Delete(category.Id).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateCategory_WhenModelIsOk_ReturnsTrueAndModelIsChanged()
        {

            // Arrange
            CategoryModel categoryModel = new()
            {
                Name = "testCategory",
                ImageUrl = "link"
            };
            using (InfrastructureContext db = new() { Connection = _connection })
            {
                db.Categories.Add(categoryModel);
                db.SaveChanges();
            }

            Category ExpectedCategory = new()
            {
                Id = categoryModel.Id,
                Name = "New Name",
                ImageUrl = categoryModel.ImageUrl,
                ParentCategoryId = categoryModel.ParentCategoryId
            };

            // Act
            var result = _service.CategoryActions.Update(ExpectedCategory).Result;

            // Assert
            Assert.True(result);

            var updatedCategory = testDatabase.Categories.AsNoTracking().FirstOrDefault(c => c.Id == ExpectedCategory.Id);
            Assert.Equivalent("New Name", updatedCategory?.Name);
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsCategoryModel()
        {

            // Arrange
            CategoryModel expectedCategory = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(expectedCategory);
            testDatabase.SaveChanges();

            // Act
            var result = _service.CategoryActions.GetById(expectedCategory.Id).Result;

            // Assert
            Assert.Equivalent(expectedCategory, result);
        }

        [Fact]
        public void CategoryActionsGetAll_WhenModelIsOk_ReturnsListCategory()
        {

            // Arrange
            List<CategoryModel> categoryModels = new()
            {
                new CategoryModel() { Name = "testCategory1" },
                new CategoryModel() { Name = "testCategory2" }
            };

            testDatabase.Categories.AddRange(categoryModels);
            testDatabase.SaveChanges();

            List<Category> expectedCategories = new()
            {
                new Category() 
                { 
                    Id = categoryModels[0].Id,
                    Name = categoryModels[0].Name,
                },
                new Category()
                {
                    Id = categoryModels[1].Id,
                    Name = categoryModels[1].Name,
                }
            };

            // Act
            var result = _service.CategoryActions.GetAll().Result;

            // Assert
            Assert.NotNull(result);

            foreach (Category category in expectedCategories)
            {
                Assert.True(result.Any(c => c.Id == category.Id && c.Name == category.Name));
            }
        }

        [Fact]
        public void AddItem_WhenModelIsOk_ReturnsId()
        {
            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();

            Item item = new() { Name = "testItem", CategoryId = category.Id };

            // Act
            var resultId = _service.ItemActions.Add(item);

            // Assert
            Assert.True(testDatabase.Items.Any());
            Assert.True(resultId.Id > 0);
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

            // Act
            var result = _service.ItemActions.Delete(item.Id).Result;

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
            ItemModel itemModel = new() { Name = "testItem", CategoryId = category.Id };
            testDatabase.Items.Add(itemModel);
            testDatabase.SaveChanges();

            Item item = new()
            { 
                Id = itemModel.Id,
                Name = "New Name",
                Description = "Description",
                CategoryId = itemModel.CategoryId,
                Amount = itemModel.Amount,
                ImageUrl = itemModel.ImageUrl,
                Price = itemModel.Price
            };

            // Act
            var result = _service.ItemActions.Update(item).Result;

            // Assert
            Assert.True(result);

            var updatedItem = testDatabase.Items.AsNoTracking().FirstOrDefault(c => c.Id == item.Id);
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
            ItemModel itemModel = new() { Name = "testItem", CategoryId = category.Id, Description = "Description" };
            testDatabase.Items.Add(itemModel);
            testDatabase.SaveChanges();

            Item item = new()
            {
                Id = itemModel.Id,
                Name = itemModel.Name,
                Description = itemModel.Description,
                CategoryId = itemModel.CategoryId,
                Amount = itemModel.Amount,
                ImageUrl = itemModel.ImageUrl,
                Price = itemModel.Price
            };

            // Act
            var result = _service.ItemActions.GetById(itemModel.Id).Result;

            // Assert
            Assert.Equivalent(item, result);
        }

        [Fact]
        public void ItemActionsGetAll_WhenModelIsOk_ReturnsListItem()
        {
            // Arrange
            CategoryModel category = new() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            List<ItemModel> itemModels = new()
            {
                new() { Name = "testitem1", CategoryId = category.Id, Description = "Description" },
                new() { Name = "testitem2", CategoryId = category.Id, Description = "Description" }
            };

            testDatabase.Items.AddRange(itemModels);
            testDatabase.SaveChanges();

            List<Item> expectedItems = new()
            {
                new()
                {
                    Id = itemModels[0].Id,
                    Name = itemModels[0].Name,
                    Description = itemModels[0].Description,
                    CategoryId = itemModels[0].CategoryId,
                    Amount = itemModels[0].Amount,
                    ImageUrl = itemModels[0].ImageUrl,
                    Price = itemModels[0].Price
                },
                new()
                {
                    Id = itemModels[1].Id,
                    Name = itemModels[1].Name,
                    Description = itemModels[1].Description,
                    CategoryId = itemModels[1].CategoryId,
                    Amount = itemModels[1].Amount,
                    ImageUrl = itemModels[1].ImageUrl,
                    Price = itemModels[1].Price
                }
            };

            // Act
            var result = _service.ItemActions.GetAll().Result;

            // Assert
            Assert.NotNull(result);
            foreach (Item item in expectedItems)
            {
                Assert.True(result.Any(c => c.Id == item.Id && c.Name == item.Name));
            }
        }
    }
}