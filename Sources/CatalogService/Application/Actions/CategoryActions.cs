﻿using Domain.Entities;
using Domain.Interfaces;

namespace Application.Actions
{
    public class CategoryActions : IActions<Category>
    {
        private readonly IRepository<Category> _repository;

        public CategoryActions(IRepository<Category> repository)
        {
            _repository = repository;
        }   

        public Task<int> Add(Category item)
        {
            return Task.FromResult(_repository.Add(item).Result);
        }

        public Task<bool> Delete(int id)
        {
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

        public Task<bool> Update(Category item)
        {
            return Task.FromResult(_repository.Update(item).Result);
        }
    }
}
