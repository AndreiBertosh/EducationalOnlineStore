﻿using Moq;
using Application.Actions;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Models;

namespace ApplicationTests
{
    public class ItemActionTests
    {
        private readonly IAzureServiceBusSendService _serviceBusSendService;

        public ItemActionTests()
        {
            _serviceBusSendService = new Mock<IAzureServiceBusSendService>().Object;
        }

        [Fact]
        public void AddItem_WhenModelIsOk_ReturnsId()
        {

            // Arrange
            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.Add(It.IsAny<Item>())).Returns(Task.FromResult(1));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            Item item = new()
            {
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            // Act
            var result = actions.Add(item).Result;

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteItem_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange
            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.Delete(It.IsAny<int>())).Returns(Task.FromResult(true));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            Item item = new()
            {
                Id = 1
            };

            // Act
            var result = actions.Delete(item.Id).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteCategory_WhenModelIsNotOk_ReturnsFalse()
        {
            // Arrange
            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.Delete(It.IsAny<int>())).Returns(Task.FromResult(false));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            Item item = new()
            {
                Id = 1,
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            // Act
            var result = actions.Delete(item.Id).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateCategory_WhenModelIsOk_ReturnsTrue()
        {
            // Arrange
            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.Update(It.IsAny<Item>())).Returns(Task.FromResult(true));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            Item item = new()
            {
                Id = 1,
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            // Act
            var result = actions.Update(item).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetById_WhenModelIsOk_ReturnsCategoryModel()
        {
            // Arrange
            Item item = new()
            {
                Id = 1,
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            Item expectedItem = new()
            {
                Id = 1,
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            }; ;

            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.GetById(It.IsAny<int>())).Returns(Task.FromResult(item));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            // Act
            var result = actions.GetById(1).Result;

            // Assert
            Assert.Equivalent(expectedItem, result);
        }

        [Fact]
        public void GetAll_WhenModelIsOk_ReturnsListCategoryModel()
        {
            // Arrange
            List<Item> items = new()
            {
                new() {
                    Id = 1,
                    Name = "Item Name 1",
                    Description = "Description 1",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new() {
                    Id = 2,
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 2,
                    Price = 20,
                }
            };

            List<Item> expectrdItems = new()
            {
                new() {
                    Id = 1,
                    Name = "Item Name 1",
                    Description = "Description 1",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new() {
                    Id = 2,
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 2,
                    Price = 20,
                }
            };

            var repository = new Mock<IRepository<Item>>();
            repository.Setup(a => a.GetAll()).Returns(Task.FromResult(items));

            ItemActions actions = new(repository.Object, _serviceBusSendService);

            // Act
            var result = actions.GetAll().Result;

            // Assert
            Assert.Equivalent(expectrdItems, result);
        }
    }
}
