using Domain.Entities;
using Domain.Models;
using Domain.Interfaces;

namespace Application.Actions
{
    public class CategoryActions : IActions<Category>
    {
        private readonly IRepository<CategoryModel> _repository;

        public CategoryActions(IRepository<CategoryModel> repository)
        {
            _repository = repository;
        }   

        public Task<int> Add(Category item)
        {
            CategoryModel model = MappingCategoryToModel(item);

            return Task.FromResult(_repository.Add(model).Result);
        }

        public Task<bool> Delete(int id)
        {
            return Task.FromResult(_repository.Delete(id).Result);
        }

        public Task<List<Category>> GetAll()
        {
            List<CategoryModel> models = _repository.GetAll().Result;

            if (models != null)
            {
                return Task.FromResult(models.Where(m => m != null).Select(m => MappingModelToCategory(m)).ToList());
            }

            return Task.FromResult(new List<Category>());
        }

        public Task<Category?> GetById(int id)
        {
            CategoryModel? model = _repository.GetById(id).Result;

            return Task.FromResult(MappingModelToCategory(model));
        }

        public Task<bool> Update(Category item)
        {
            return Task.FromResult(_repository.Update(MappingCategoryToModel(item)).Result);
        }

        private static CategoryModel MappingCategoryToModel(Category category)
        {
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                ParentCategoryId = category.ParentCategoryId
            };
        }

        private static Category? MappingModelToCategory(CategoryModel? model)
        {
            if (model != null)
            {
                return new Category
                {
                    Id = model.Id,
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    ParentCategoryId = model.ParentCategoryId
                };
            }
            return null;
        }
    }
}
