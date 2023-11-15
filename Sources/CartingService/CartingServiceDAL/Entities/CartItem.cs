﻿using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Entities
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public List<CartField> Fields { get; set; } 
        public string Image { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}