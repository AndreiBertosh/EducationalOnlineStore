using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using CartEntity = CartingServiceBusinessLogic.Infrastructure.Entities.CartEntity;

namespace CartingServiceBusinessLogic
{
    public class CartService
    {
        public CartEntity _cartEntity;

        public CartActions<CartEntity> CartActions;

        public CartService(string databaseName, string cartName)
        {
            this.CartActions = new CartActions<CartEntity>(databaseName, cartName);
        }
    }
}
