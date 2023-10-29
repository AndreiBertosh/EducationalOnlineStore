using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<CategoryModel>
    {
        private readonly InfrastructureContext _context;

        public CategoryRepository(InfrastructureContext context)
        {
            _context = context;
        }

        public Task<int> Add(CategoryModel item)
        {
            _context.Categories.Add(item);
            _context.SaveChanges();
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

        public Task<List<CategoryModel>> GetAll()
        {
            var result = _context.Categories.ToList();
            return Task.FromResult(result);
        }

        public Task<CategoryModel?> GetById(int id)
        {
            var result = _context.Categories.Find(id);
            return Task.FromResult(result);
        }

        public Task<bool> Update(CategoryModel item)
        {
            _context.Categories.Update(item);
            return Task.FromResult(true);
        }
    }
}