using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Entities
{
    public class CartItemModel : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}