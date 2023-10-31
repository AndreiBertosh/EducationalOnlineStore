using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IRepository<ItemModel>
    {
        private readonly InfrastructureContext _context;

        public ItemRepository(InfrastructureContext context)
        {
            _context = context;
        }

        public Task<int> Add(ItemModel item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return Task.FromResult(item.Id);
        }

        public Task<bool> Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<ItemModel>> GetAll()
        {
            var result = _context.Items.ToList();
            return Task.FromResult(result);
        }

        public Task<ItemModel?> GetById(int id)
        {
            var result = _context.Items.Find(id);
            return Task.FromResult(result);
        }

        public Task<bool> Update(ItemModel item)
        {
            _context.Items.Update(item);
            _context.SaveChanges(true);
            return Task.FromResult(true);
        }
    }
}