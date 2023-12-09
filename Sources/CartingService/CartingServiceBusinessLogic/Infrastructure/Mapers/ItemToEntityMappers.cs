using AutoMapper;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using System.Data.Common;

namespace CartingServiceBusinessLogic.Infrastructure.Mapers
{
    public static class ItemToEntityMappers
    {
        public static Mapper ItemToModelMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<CartItemModel, CartItem>().ReverseMap());
            return new(mapConfig);
        }

        public static Mapper EntityToModelMapper()
        {
            MapperConfiguration mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartEntity, CartModel>().ReverseMap();
                cfg.CreateMap<CartItem, CartItemModel>().ReverseMap();
            });

            return new(mapConfig);
        }
    }
}
