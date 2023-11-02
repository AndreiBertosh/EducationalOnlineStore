using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ItemModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
