using AutoMapper;
using CartingServiceBusinessLogic.Infrastructure.Entities;

namespace CartingServiceBusinessLogic.Infrastructure.Mapers
{
    public static class ItemToCartItemMappers
    {
        public static Mapper ItemToCartItemMapper()
        {
            MapperConfiguration mapConfig = new(cfg => cfg.CreateMap<CartItem, Item>()
            .ForMember(dest => dest.Amount, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ReverseMap());
            return new(mapConfig);
        }

    }
}
