using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InfrastructureContext : DbContext
    {
        private string _connection = string.Empty;

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