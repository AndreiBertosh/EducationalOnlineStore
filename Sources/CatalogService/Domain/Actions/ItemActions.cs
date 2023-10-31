using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Interfaces;

namespace Domain.Actions
{
    public class ItemActions : IActions<Item>
    {
        private readonly IRepository<ItemModel> _repository;

        public ItemActions(IRepository<ItemModel> repository)
        {
            _repository = repository;
        }

        public Task<int> Add(Item item)
        {
            ItemModel model = MappingItemToModel(item);

            return Task.FromResult(_repository.Add(model).Result);
        }

        public Task<bool> Delete(int id)
        {
            return Task.FromResult(_repository.Delete(id).Result);
        }

        public Task<List<Item>> GetAll()
        {
            List<ItemModel> models = _repository.GetAll().Result;

            if (models != null)
            {
                return Task.FromResult(models.Where(m => m != null).Select(m => MappingModelToItem(m)).ToList());
            }

            return Task.FromResult(new List<Item>());
        }

        public Task<Item?> GetById(int id)
        {
            ItemModel? model = _repository.GetById(id).Result;

            return Task.FromResult(MappingModelToItem(model));
        }

        public Task<bool> Update(Item item)
        {
            return Task.FromResult(_repository.Update(MappingItemToModel(item)).Result);
        }

        private static ItemModel MappingItemToModel(Item item)
        {
            return new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                CategoryId = item.CategoryId,
                Amount = item.Amount,
                Price = item.Price
            };
        }

        private static Item? MappingModelToItem(ItemModel? model)
        {
            if (model != null)
            {
                return new Item
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                    Amount = model.Amount,
                    Price = model.Price
                };
            }
            return null;
        }
    }
}
