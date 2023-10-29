﻿using Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class CategoryModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int ParentCategoryId { get; set; }
    }
}
