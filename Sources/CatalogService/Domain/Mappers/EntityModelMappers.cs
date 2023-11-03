using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Domain.Mappers
{
    public static class EntityModelMappers
    {
        public static Mapper CategoryToModelMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<Category, CategoryModel>());
            return new(mapConfig);
        }

        public static Mapper ModelToCategoryMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<CategoryModel, Category>());
            return new(mapConfig);
        }

        public static Mapper ItemToModelMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<Item, ItemModel>());
            return new(mapConfig);
        }

        public static Mapper ModelToItemMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<ItemModel, Item>());
            return new(mapConfig);
        }
    }
}
