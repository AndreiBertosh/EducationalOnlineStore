using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;

namespace CartingService
{
    public class CartActions<T> : ICartActions<T>
        where T : CartItem
    {
        private readonly string _databaseName = string.Empty;
        private readonly string _collectionName = string.Empty;
        private CartRepository<CartItem> _cartRepository;

        public CartActions(string databaseName, string collectionName)
        {
            _cartRepository = new CartRepository<CartItem>(databaseName, collectionName);
        }

        public Task<int> AddToChart(T item)
        {
            var id = _cartRepository.Add(item).Result;

            return Task.FromResult(id);
        }

        public Task<List<T>> GetListItems()
        {
            var result = _cartRepository.GetAll().Result.Select(e => (T)e).ToList();

            return Task.FromResult(result);
        }

        public Task<bool> RemoevFromChart(T item)
        {
            return Task.FromResult(_cartRepository.Delete(item).Result);
        }
    }
}