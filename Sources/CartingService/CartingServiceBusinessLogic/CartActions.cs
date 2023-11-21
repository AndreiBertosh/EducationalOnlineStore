using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Mapers;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;

namespace CartingService
{
    public class CartActions<T> : ICartActions<T>
        where T : CartItem
    {
        private readonly CartRepository<CartItemModel> _cartRepository;

        public CartActions(string databaseName, string collectionName)
        {
            _cartRepository = new CartRepository<CartItemModel>(databaseName, collectionName);
        }

        public Task<int> AddToCart(T item)
        {

            var id = _cartRepository.Add(ItemToEntityMappers.ItemToModelMapper().Map<CartItemModel>(item)).Result;

            return Task.FromResult(id);
        }

        public Task<List<T>> GetListItems()
        {
            var result = ItemToEntityMappers.ItemToModelMapper().Map<List<CartItem>>(_cartRepository.GetAll().Result).Select(e => (T)e).ToList();
            return Task.FromResult(result);
        }

        public Task<bool> RemoevFromCart(T item)
        {
            return Task.FromResult(_cartRepository.Delete(ItemToEntityMappers.ItemToModelMapper().Map<CartItemModel>(item)).Result);
        }
    }
}