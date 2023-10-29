using CatalogService.Infrastructure.Interfaces;

namespace CatalogService.Domain.Entities
{
    public class Category : IItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public object Image { get; set; }

        public int ParentCategoryId { get; set; }
    }
}