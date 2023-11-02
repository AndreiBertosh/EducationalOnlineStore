namespace Domain.Interfaces
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<int> Add(T item);

        Task<bool> Delete(int id);

        Task<T?> GetById(int id);

        Task<List<T>> GetAll();

        Task<bool> Update(T item);
    }
}