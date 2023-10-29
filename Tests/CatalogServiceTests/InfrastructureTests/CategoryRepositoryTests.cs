using Infrastructure;
using Infrastructure.Models;
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
            CategoryModel category = new CategoryModel() { Name = "testCategory" };
            var repository = new CategoryRepository(testDatabase);

            // Act
            repository.Add(category);

            // Assert
            Assert.True(testDatabase.Categories.Any());
            Assert.True(category.Id > 0);
        }

        [Fact]
        public void DeleteCategory_WhenModelIsOk_ReturnsTrue()
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
        public void UpdateCategory_WhenModelIsOk_ReturnsTrueAndModelIsChanged()
        {

            // Arrange
            CategoryModel category = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(category);
            testDatabase.SaveChanges();

            category.Name = "New Name";
            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.Update(category).Result;

            // Assert
            Assert.True(result);

            var updatedCategory = testDatabase.Categories.Find(category.Id);
            Assert.Equivalent(category.Name, updatedCategory?.Name);
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsCategoryModel()
        {

            // Arrange
            CategoryModel expectedCategory = new CategoryModel() { Name = "testCategory" };
            testDatabase.Categories.Add(expectedCategory);
            testDatabase.SaveChanges();

            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.GetById(expectedCategory.Id).Result;

            // Assert
            Assert.Equivalent(expectedCategory, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListCategoryModel()
        {

            // Arrange
            List<CategoryModel> expectrdCategoryModels = new List<CategoryModel>
            {
                new CategoryModel() { Name = "testCategory1" },
                new CategoryModel() { Name = "testCategory2" }
            };

            testDatabase.Categories.AddRange(expectrdCategoryModels);
            testDatabase.SaveChanges();

            var repository = new CategoryRepository(testDatabase);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            var intersected = result.Intersect(expectrdCategoryModels).ToList();
            Assert.Equivalent(expectrdCategoryModels, intersected);
        }
    }
}