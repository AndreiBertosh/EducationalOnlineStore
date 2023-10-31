using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Item> Items => Set<Item>();

        public ApplicationContext() => Database.EnsureCreated();
    }
}