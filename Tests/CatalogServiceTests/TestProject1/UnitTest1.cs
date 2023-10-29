using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            using InfrastructureContext db = new InfrastructureContext();
            db.Database.Migrate();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            CategoryModel category = new CategoryModel() { Name = "testCategory" };
            db.Categories.Add(category);
            db.SaveChanges();

            ItemModel item = new ItemModel() { Name = "test", Description = "description", CategoryId = category.Id };
            db.Items.Add(item);
            db.SaveChanges();

            // Act

            // Assert

        }
    }
}