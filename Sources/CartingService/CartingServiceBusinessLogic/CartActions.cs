using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Mapers;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;

namespace CartingService
{
    public class CartActions<T> : ICartActions<T>
        where T : CartEntity
    {
        private readonly CartRepository<CartItem> _cartRepository;

        public CartActions(string databaseName, string collectionName)
        {
            _cartRepository = new CartRepository<CartItem>(databaseName, collectionName);
        }

        public Task<int> AddToCart(T item)
        {

            var id = _cartRepository.Add(ItemToEntityMappres.EntityToItemMapper().Map<CartItem>(item)).Result;

            return Task.FromResult(id);
        }

        public Task<List<T>> GetListItems()
        {
            var result = ItemToEntityMappres.ItemToEntitylMapper().Map<List<CartEntity>>(_cartRepository.GetAll().Result).Select(e => (T)e).ToList();
            return Task.FromResult(result);
        }

        public Task<bool> RemoevFromCart(T item)
        {
            return Task.FromResult(_cartRepository.Delete(ItemToEntityMappres.EntityToItemMapper().Map<CartItem>(item)).Result);
        }
    }
}