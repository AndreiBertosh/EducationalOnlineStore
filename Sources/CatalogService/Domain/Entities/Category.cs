using Domain.Interfaces;

namespace Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int ParentCategoryId { get; set; }
    }
}