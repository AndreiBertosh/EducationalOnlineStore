﻿using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InfrastructureContext : DbContext
    {
        private string _connection = string.Empty;

        public InfrastructureContext()
        { 
        }

        public InfrastructureContext(string connectionString)
        {
            _connection = connectionString;
        }

        public string Connection 
        { 
            set
            {
                _connection = value;
            }
         } 

        public DbSet<ItemModel> Items => Set<ItemModel>();

        public DbSet<CategoryModel> Categories => Set<CategoryModel>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}