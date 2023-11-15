using Domain.Entities;
using Domain.Interfaces;

namespace Application.Actions
{
    public class ItemActions : IActionsItem<Item>
    {
        private readonly IRepository<Item> _repository;

        public ItemActions(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public Task<int> Add(Item item)
        {
            return Task.FromResult(_repository.Add(item).Result);
        }

        public Task<bool> Delete(int id)
        {
            return Task.FromResult(_repository.Delete(id).Result);
        }

        public Task<List<Item>> GetAll()
        {
            return Task.FromResult(_repository.GetAll().Result);
        }

        public Task<Item?> GetById(int id)
        {
            return Task.FromResult(_repository.GetById(id).Result);
        }

        public Task<bool> Update(Item item)
        {
            return Task.FromResult(_repository.Update(item).Result);
        }

        public Task<bool> DeleteAllItemsForCategoryId(int categoryId)
        {
            var items = GetAllItemsForCategoryId(categoryId).Result;
            items.ForEach(item => _repository.Delete(item.Id));

            return Task.FromResult(true);
        }

        public Task<List<Item>> GetAllItemsForCategoryId(int categoryId)
        {
            return Task.FromResult(_repository.GetAll().Result.Where(item => item.CategoryId == categoryId).ToList());
        }

        public Task<List<Item>> GetItems(int skipItems, int count)
        {
            var items = _repository.GetAll().Result;
            return Task.FromResult(items.Skip(skipItems).Take(count).ToList());
        }
    }
}
