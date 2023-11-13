using Moq;
using Application.Actions;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Models;

namespace ApplicationTests
{
    public class CategoryActionTests
    {

        [Fact]
        public void AddCategory_WhenModelIsOk_ReturnsId()
        {
            // Arrange
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.Add(It.IsAny<Category>())).Returns(Task.FromResult(1));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            Category category = new Category
            { 
                Name = "Category Name"
            };

            // Act
            var result = actions.Add(category).Result;

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteCategory_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.Delete(It.IsAny<int>())).Returns(Task.FromResult(true));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            Category category = new Category
            {
                Id = 1
            };

            // Act
            var result = actions.Delete(category.Id).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteCategory_WhenModelIsNotOk_ReturnsFalse()
        {
            // Arrange
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.Delete(It.IsAny<int>())).Returns(Task.FromResult(false));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            Category category = new Category
            {
                Id = 1
            };

            // Act
            var result = actions.Delete(category.Id).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateCategory_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.Update(It.IsAny<Category>())).Returns(Task.FromResult(true));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            Category category = new()
            {
                Id = 1,
                Name = "Category",
                ImageUrl = string.Empty
            };

            // Act
            var result = actions.Update(category).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsCategoryModel()
        {
            // Arrange
            Category category = new()
            {
                Id = 1,
                Name = "Category model",
                ImageUrl = string.Empty
            };

            Category expectrdCategory = new()
            {
                Id = 1,
                Name = "Category model",
                ImageUrl = string.Empty
            };

            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.GetById(It.IsAny<int>())).Returns(Task.FromResult(category));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            // Act
            var result = actions.GetById(1).Result;

            // Assert
            Assert.Equivalent(expectrdCategory, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListCategoryModel()
        {
            // Arrange
            List<Category> categories = new()
            {
                new() {
                    Id = 1, 
                    Name = "Category model",
                    ImageUrl = string.Empty
                },
                new() {
                    Id = 2,
                    Name = "Category model 2",
                    ImageUrl = string.Empty
                }
            };

            List<Category> expectrdCategories = new()
            {
                new() {
                    Id = 1,
                    Name = "Category model",
                    ImageUrl = string.Empty
                },
                new() {
                    Id = 2,
                    Name = "Category model 2",
                    ImageUrl = string.Empty
                }
            };

            var repository = new Mock<IRepository<Category>>();
            repository.Setup(a => a.GetAll()).Returns(Task.FromResult(categories));

            var itemActions = new Mock<ItemActions>();
            CategoryActions actions = new CategoryActions(repository.Object, itemActions.Object);

            // Act
            var result = actions.GetAll().Result;

            // Assert
            Assert.Equivalent(expectrdCategories, result);
        }
    }
}