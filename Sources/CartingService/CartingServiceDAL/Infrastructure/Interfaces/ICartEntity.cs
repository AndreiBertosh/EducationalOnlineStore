using CartingServiceDAL.Entities;

namespace CartingServiceDAL.Infrastructure.Interfaces
{
    internal interface ICartEntity : IEntity
    {
        string Name { get; set; }

        List<CartItem> Entities { get; set; }
    }
}
