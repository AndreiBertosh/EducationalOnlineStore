using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic.Infrastructure.Interfaces
{
    public interface ICartActions<T>
        where T : IEntity
    {
        Task<int> AddToCart(T item);

        Task<bool> RemoevFromCart(T item);

        Task<List<T>> GetListItems();
    }
}
