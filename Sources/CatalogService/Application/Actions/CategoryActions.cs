using Domain.Entities;
using Domain.Interfaces;

namespace Application.Actions
{
    public class CategoryActions : IActions<Category>
    {
        private readonly IRepository<Category> _repository;
        private readonly IActionsItem<Item> _itemActions;

        public CategoryActions(IRepository<Category> repository, IActionsItem<Item> itemActions)
        {
            _repository = repository;
            _itemActions = itemActions;
        }   

        public Task<int> Add(Category item)
        {
            return Task.FromResult(_repository.Add(item).Result);
        }

        public Task<bool> Delete(int id)
        {
            _itemActions.DeleteAllItemsForCategoryId(id);
            return Task.FromResult(_repository.Delete(id).Result);
        }

        public Task<List<Category>> GetAll()
        {
            return Task.FromResult(_repository.GetAll().Result);
        }

        public Task<Category?> GetById(int id)
        {
            return Task.FromResult(_repository.GetById(id).Result);
        }

        public Task<string> Update(Category item)
        {
            string result = string.Empty;
            if (_repository.Update(item).Result)
            {
                result = $"The category {item.Name} was updated.";
            }
            else
            {
                result = $"The category {item.Name} was not updated!!!";
            };
            return Task.FromResult(result);
        }
    }
}
