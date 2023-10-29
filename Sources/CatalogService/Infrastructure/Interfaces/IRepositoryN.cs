namespace Infrastructure.Interfaces
{
    public interface IRepositoryN<T>
        where T : IEntity
    {
        Task<int> Add(T item);

        Task<bool> Delete(T item);

        Task<T> GetById(int id);

        Task<List<T>> GetAll();

        Task<bool> Update(T item);
    }
}
