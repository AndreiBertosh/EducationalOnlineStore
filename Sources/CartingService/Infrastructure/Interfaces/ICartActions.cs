using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic.Infrastructure.Interfaces
{
    public interface ICartActions<T>
        where T : IEntity
    {
        Task<int> AddToChart(T item);

        Task<bool> RemoevFromChart(T item);

        Task<List<T>> GetListItems();
    }
}
