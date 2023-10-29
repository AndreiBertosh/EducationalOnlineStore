using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Interfaces;

namespace CatalogService.Infrastructure.Repositories
{
    internal class ItemRepository<T> : IRepository<T>
        where T : Item
    {
        public Task<int> Add(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
