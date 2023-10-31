using CatalogService.Infrastructure.Interfaces;

namespace CatalogService.Domain.Entities
{ 
    public class Item : IItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Image { get; set; }

        public int CategoryId { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
