using Domain.Entities;
using Domain.Interfaces;

namespace Application.Actions
{
    public class ItemActions : IActions<Item>
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
    }
}
