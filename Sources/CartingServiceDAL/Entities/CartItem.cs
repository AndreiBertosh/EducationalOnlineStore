using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Entities
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public object Image { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}