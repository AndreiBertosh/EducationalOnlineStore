using CartingServiceBusinessLogic.Infrastructure.Entities;

namespace CartingServiceBusinessLogic.Infrastructure.Interfaces
{
    public interface ICartActionsNew<CartEntity>
    {
        Task<int> AddToCart(CartEntity item);

        Task<bool> RemoevFromCart(string cartName, int itemId);

        Task<CartEntity> GetCart(string cartName);

        Task<bool> ItemsUpdate(CartItem cartItem);
    }
}
