using CartingServiceDAL.Entities;

namespace CartingServiceDAL.Infrastructure.Interfaces
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<int> Add(T item);

        Task<bool> Delete(T item);

        Task<T?> GetById(int id);

        Task<List<T>> GetAll();

        Task<T> Update(T item);
    }
}
