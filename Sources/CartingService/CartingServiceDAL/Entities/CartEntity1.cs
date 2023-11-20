using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Entities
{
    public class CartEntity1 : ICartEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CartItem> Entities { get; set; }
    }
}
