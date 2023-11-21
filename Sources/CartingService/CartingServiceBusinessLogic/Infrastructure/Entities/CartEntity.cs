using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic.Infrastructure.Entities
{
    public class CartEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CartItem> Items { get; set; }
    }
}