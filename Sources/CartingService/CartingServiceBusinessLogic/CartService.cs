using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceDAL.Entities;
using CartItem = CartingServiceBusinessLogic.Infrastructure.Entities.CartItem;

namespace CartingServiceBusinessLogic
{
    public class CartService
    {
        public CartItem _cartEntity;

        public CartActions<CartItem> CartActions;
        public readonly CartActionsNew _cartActions;

        public CartService(string databaseName, string cartName)
        {
            this.CartActions = new CartActions<CartItem>(databaseName, cartName);

            _cartActions = new CartActionsNew(databaseName, cartName);
        }
    }
}
