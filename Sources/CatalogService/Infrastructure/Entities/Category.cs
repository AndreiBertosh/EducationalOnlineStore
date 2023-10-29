using Infrastructure.Interfaces;

namespace Infrastructure.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public object Image { get; set; }

        public int ParentCategoryId { get; set; }
    }
}