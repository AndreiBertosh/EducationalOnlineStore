namespace CatalogService.Infrastructure.Interfaces
{
    public interface IRepository<T>
        where T : IItem
    {
        Task<int> Add(T item);

        Task<bool> Delete(T item);

        Task<T?> GetById(int id);

        Task<IList<T>> GetAll();

        Task<T> Update(T item);
    }
}
