using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Entities
{
    public class CartModel : ICartEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CartItemModel> Items { get; set; }
    }
}
