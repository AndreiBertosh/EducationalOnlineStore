using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Mapers;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Repository;

namespace CartingServiceBusinessLogic
{
    public class CartActionsNew : ICartActionsNew<CartEntity>
    {
        private readonly CartRepositoryFull _cartRepository;

        public CartActionsNew(string databaseName, string collectionName)
        {
            _cartRepository = new CartRepositoryFull(databaseName, collectionName);
        }

        public Task<int> AddToCart(CartEntity cart)
        {
            var model = _cartRepository.Update(ItemToEntityMappers.EntityToModelMapper().Map<CartModel>(cart)).Result;
            var result = ItemToEntityMappers.EntityToModelMapper().Map<CartEntity>(model);

            return Task.FromResult(result.Id);
        }

        public Task<CartEntity> GetCart(string cartName)
        {
            CartModel model = _cartRepository.GetAll(cartName).Result;
            CartEntity result = ItemToEntityMappers.EntityToModelMapper().Map<CartEntity>(model);

            return Task.FromResult(result);
        }

        public Task<bool> RemoevFromCart(string cartName, int itemId)
        {
            bool result = _cartRepository.Delete(cartName, itemId).Result;

            return Task.FromResult(result);
        }

        public Task<bool> ItemsUpdate(CartItem cartItem)
        {
            CartItemModel model = ItemToEntityMappers.ItemToModelMapper().Map<CartItemModel>(cartItem);
            bool result = _cartRepository.ItemsUpdate(model).Result;
            return Task.FromResult(result);
        }
    }
}
