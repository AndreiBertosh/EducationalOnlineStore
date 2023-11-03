using Domain.Entities;
using Domain.Models;
using Infrastructure.Mappers;

namespace DomainTests
{
    public class ItemMappersTests
    {
        [Fact]
        public void ItemToModelMapper_WhenMapOneItem_ReturnsModel()
        {
            // Arrange
            Item item = new()
            {
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            ItemModel expectedItemModel = new()
            {
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            // Act
            var result = EntityModelMappers.ItemToModelMapper().Map<ItemModel>(item);

            // Assert
            Assert.Equivalent(expectedItemModel, result);
        }

        [Fact]
        public void ItemToModelMapper_WhenMapListIlems_ReturnsModelList()
        {
            // Arrange
            List<Item> items = new()
            {
                new()
                {
                    Name = "Item Name",
                    Description = "Description",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new()
                {
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url2",
                    CategoryId = 2,
                    Amount = 1,
                    Price = 100,
                }
            };

            List<ItemModel> expectedItemModels = new()
            {
                 new()
                {
                    Name = "Item Name",
                    Description = "Description",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new()
                {
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url2",
                    CategoryId = 2,
                    Amount = 1,
                    Price = 100,
                }
            };

            // Act
            var result = EntityModelMappers.ItemToModelMapper().Map<List<ItemModel>>(items);

            // Assert
            Assert.Equivalent(expectedItemModels, result);
        }

        [Fact]
        public void ModelToItemMapper_WhenMapOneItemModel_ReturnsItem()
        {
            // Arrange
            ItemModel itemModel = new()
            {
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            Item expectedItem = new()
            {
                Name = "Item Name",
                Description = "Description",
                ImageUrl = "url",
                CategoryId = 1,
                Amount = 1,
                Price = 10,
            };

            // Act
            var result = EntityModelMappers.ModelToItemMapper().Map<Item>(itemModel);

            // Assert
            Assert.Equivalent(expectedItem, result);
        }

        [Fact]
        public void ModelToItemMapper_WhenMapListItemModels_ReturnsItemModelist()
        {
            // Arrange
            List<ItemModel> itemModels = new()
            {
                new()
                {
                    Name = "Item Name",
                    Description = "Description",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new()
                {
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url2",
                    CategoryId = 2,
                    Amount = 1,
                    Price = 100,
                }
            };

            List<Item> expectedItems = new()
            {
                new()
                {
                    Name = "Item Name",
                    Description = "Description",
                    ImageUrl = "url",
                    CategoryId = 1,
                    Amount = 1,
                    Price = 10,
                },
                new()
                {
                    Name = "Item Name 2",
                    Description = "Description 2",
                    ImageUrl = "url2",
                    CategoryId = 2,
                    Amount = 1,
                    Price = 100,
                }
            };

            // Act
            var result = EntityModelMappers.ModelToItemMapper().Map<List<ItemModel>>(itemModels);

            // Assert
            Assert.Equivalent(expectedItems, result);
        }
    }
}
