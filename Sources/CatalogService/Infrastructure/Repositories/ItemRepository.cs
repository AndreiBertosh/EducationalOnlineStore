using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly InfrastructureContext _context;

        public ItemRepository(InfrastructureContext context)
        {
            _context = context;
        }

        public Task<int> Add(Item item)
        {
            var model = EntityModelMappers.ItemToModelMapper().Map<ItemModel>(item);

            _context.Items.Add(model);
            _context.SaveChanges();
            item.Id = model.Id;

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

        public Task<List<Item>> GetAll()
        {
            var result = _context.Items.ToList();
            return Task.FromResult(EntityModelMappers.ModelToItemMapper().Map<List<Item>>(result));
        }

        public Task<Item?> GetById(int id)
        {
            var result = _context.Items.Find(id);
            return Task.FromResult(EntityModelMappers.ModelToItemMapper().Map<Item>(result));
        }

        public Task<bool> Update(Item item)
        {
            var model = _context.Items.Find(item.Id);
            if (model != null)
            {
                model.Name = item.Name;
                model.Description = item.Description;
                model.Price = item.Price;
                model.CategoryId = item.CategoryId;
                model.ImageUrl = item.ImageUrl;
                model.Amount = item.Amount;

                _context.Items.Update(model);
                _context.SaveChanges(true);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}