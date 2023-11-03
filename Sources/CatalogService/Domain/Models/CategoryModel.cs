using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CategoryModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int ParentCategoryId { get; set; }
    }
}
