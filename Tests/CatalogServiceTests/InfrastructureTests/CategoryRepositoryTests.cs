using Domain.Interfaces;
using Domain.Models;
using Domain.Entities;
using Infrastructure.Mappers;
using Infrastructure;
using Infrastructure.Repositories;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace InfrastructureTests
{
    public class CategoryRepositoryTests : IDisposable
    {
        private readonly string connection = @"data source=(localdb)\MSSQLLocalDB;Initial Catalog=TestCatalogDb;Integrated Security=True;";
        private readonly InfrastructureContext testDatabase;


        public CategoryRepositoryTests()
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
        public void AddCategory_WhenModelIsOk_ReturnsId()
        {

            // Arrange
            Category category = new() 
            { 
                Name = "testCategory" 
            };
            var repository = new CategoryRepository(testDatabase);
            var expectedRecordsCount = testDatabase.Categories.Count() + 1;

            // Act
            var id = repository.Add(category).Result;

            // Assert
            Assert.True(testDatabase.Categories.Any());
            Assert.True(id > 0);
            var recordsCount = testDatabase.Categories.Count();
            Assert.Equal(expectedRecordsCount, recordsCount);
        }

        [Fact]
        public void DeleteCategory_WhenRecordFound_ReturnsTrue()
        {
            // Arrange
            CategoryModel category = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.Delete(category.Id).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteCategory_WhenRecordNotFound_ReturnsFalse()
        {
            // Arrange
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.Delete(1000000).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateCategory_WhenModelIsOk_ReturnsTrueAndModelIsChanged()
        {

            // Arrange
            CategoryModel categoryModel = new()
            { 
                Name = "testCategory",
                ImageUrl = string.Empty
            };
            testDatabase.Categories.Add(categoryModel);
            testDatabase.SaveChanges();

            Category category = new()
            {
                Id = categoryModel.Id,
                Name = "New Name",
                ImageUrl = string.Empty
            };
            
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.Update(category).Result;

            // Assert
            Assert.True(result);

            var updatedCategory = testDatabase.Categories.Find(categoryModel.Id);
            Assert.Equivalent("New Name", updatedCategory?.Name);
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsCategoryModel()
        {

            // Arrange
            CategoryModel categoryModel = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(categoryModel);
            testDatabase.SaveChanges();

            var expectedCategory = EntityModelMappers.ModelToCategoryMapper().Map<Category>(categoryModel);
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.GetById(categoryModel.Id).Result;

            // Assert
            Assert.Equivalent(expectedCategory, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListCategoryModel()
        {

            // Arrange
            List<CategoryModel> categoryModels = new List<CategoryModel>
            {
                new CategoryModel() { Name = "testCategory1" },
                new CategoryModel() { Name = "testCategory2" }
            };

            testDatabase.Categories.AddRange(categoryModels);
            testDatabase.SaveChanges();

            var expectedCategories = EntityModelMappers.ModelToCategoryMapper().Map<List<Category>>(categoryModels);
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            foreach (Category category in expectedCategories)
            {
                Assert.True(result.Any(c => c.Id == category.Id && c.Name == category.Name));
            }
        }
    }
}