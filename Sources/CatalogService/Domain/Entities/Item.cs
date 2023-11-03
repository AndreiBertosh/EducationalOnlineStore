using Domain.Interfaces;

namespace Domain.Entities
{ 
    public class Item : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
