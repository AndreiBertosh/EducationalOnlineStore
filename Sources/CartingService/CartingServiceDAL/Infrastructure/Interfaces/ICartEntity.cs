using CartingServiceDAL.Entities;

namespace CartingServiceDAL.Infrastructure.Interfaces
{
    internal interface ICartEntity : IEntity
    {
        string Name { get; set; }

        List<CartItemModel> Items { get; set; }
    }
}
