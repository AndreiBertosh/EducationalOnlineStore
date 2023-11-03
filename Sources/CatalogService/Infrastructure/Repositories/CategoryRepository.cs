using Domain.Interfaces;
using Domain.Models;
using Domain.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly InfrastructureContext _context;

        public CategoryRepository(InfrastructureContext context)
        {
            _context = context;
        }

        public Task<int> Add(Category item)
        {
            var model = EntityModelMappers.CategoryToModelMapper().Map<CategoryModel>(item);

            _context.Categories.Add(model);
            _context.SaveChanges();

            item.Id = model.Id;
            return Task.FromResult(item.Id);
        }

        public Task<bool> Delete(int id)
        {
            var item = _context.Categories.Find(id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<Category>> GetAll()
        {
            var result = _context.Categories.ToList();
            return Task.FromResult(EntityModelMappers.ModelToCategoryMapper().Map<List<Category>>(result));
        }

        public Task<Category?> GetById(int id)
        {
            var result = _context.Categories.Find(id);
            return Task.FromResult(EntityModelMappers.ModelToCategoryMapper().Map<Category>(result));
        }

        public Task<bool> Update(Category item)
        {
            var model = _context.Categories.Find(item.Id);
            
            model.Name = item.Name;
            model.ImageUrl = item.ImageUrl;
            model.ParentCategoryId = item.ParentCategoryId;

            _context.Categories.Update(model);
            _context.SaveChanges(false);
            return Task.FromResult(true);
        }
    }
}