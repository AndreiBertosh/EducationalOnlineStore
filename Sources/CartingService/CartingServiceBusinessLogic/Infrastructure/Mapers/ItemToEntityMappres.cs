using AutoMapper;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;

namespace CartingServiceBusinessLogic.Infrastructure.Mapers
{
    public static class ItemToEntityMappres
    {
        public static Mapper ItemToEntitylMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<CartItem, CartEntity>());
            return new(mapConfig);
        }

        public static Mapper EntityToItemMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<CartEntity, CartItem>());
            return new(mapConfig);
        }
    }
}
