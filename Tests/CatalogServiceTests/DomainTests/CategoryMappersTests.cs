using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests
{
    public class CategoryMappersTests
    {
        [Fact]
        public void CategoryToModelMapper_WhenMapOneCategory_ReturnsModel()
        {
            // Arrange
            Category category = new()
            {
                Id = 1,
                Name = "Category",
                ImageUrl = string.Empty
            };

            CategoryModel expectedCategoryModel = new()
            {
                Id = 1,
                Name = "Category",
                ImageUrl = string.Empty
            };

            // Act
            var result = EntityModelMappers.CategoryToModelMapper().Map<CategoryModel>(category);

            // Assert
            Assert.Equivalent(expectedCategoryModel, result);
        }

        [Fact]
        public void CategoryToModelMapper_WhenMapListCategories_ReturnsModelList()
        {
            // Arrange
            List<Category> categories = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Category1",
                    ImageUrl = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Category2",
                    ImageUrl = string.Empty
                }
            };

            List<CategoryModel> expectedCategoryModels = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Category1",
                    ImageUrl = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Category2",
                    ImageUrl = string.Empty
                }
            };

            // Act
            var result = EntityModelMappers.CategoryToModelMapper().Map<List<CategoryModel>>(categories);

            // Assert
            Assert.Equivalent(expectedCategoryModels, result);
        }

        [Fact]
        public void ModelToCategoryMapper_WhenMapOneCategoryModel_ReturnsCategory()
        {
            // Arrange
            CategoryModel categoryModel = new()
            {
                Id = 1,
                Name = "Category",
                ImageUrl = string.Empty
            };

            Category expectedCategory = new()
            {
                Id = 1,
                Name = "Category",
                ImageUrl = string.Empty
            };

            // Act
            var result = EntityModelMappers.ModelToCategoryMapper().Map<Category>(categoryModel);

            // Assert
            Assert.Equivalent(expectedCategory, result);
        }

        [Fact]
        public void ModelToCategoryMapper_WhenMapListCategoryModels_ReturnsCategoryList()
        {
            // Arrange
            List<CategoryModel> categoryModels = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Category1",
                    ImageUrl = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Category2",
                    ImageUrl = string.Empty
                }
            };

            List<Category> expectedCategories = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Category1",
                    ImageUrl = string.Empty
                },
                new()
                {
                    Id = 2,
                    Name = "Category2",
                    ImageUrl = string.Empty
                }
            };

            // Act
            var result = EntityModelMappers.ModelToCategoryMapper().Map<List<CategoryModel>>(categoryModels);

            // Assert
            Assert.Equivalent(expectedCategories, result);
        }
    }
}
