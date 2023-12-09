using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic.Infrastructure.Entities
{
    public class CartItem : IEntity
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public string ImageUrl { get; set; }

            public int Price { get; set; }

            public int Quantity { get; set; }
    }
}
