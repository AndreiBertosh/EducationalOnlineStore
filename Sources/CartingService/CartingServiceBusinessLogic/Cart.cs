using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic
{
    public class Cart : ICart
    {
        private string _cartName = string.Empty;
        private readonly CartActions<CartItem> _cartService;
        //private readonly CartService _cartService;

        public Cart(string cartName, string databaseName, string collectionName)
        {
            _cartService = new CartActions<CartItem>(databaseName, collectionName);
            //_cartService = new CartService(databaseName, collectionName);
            CartName = cartName;
        }

        public string CartName
        {
            get
            {
                return _cartName;
            }
            set
            {
                _cartName = value;
            }
        }

        public List<CartItem> Items
        {
            get
            {
//                return  _cartService.CartActions.GetListItems().Result;
                return _cartService.GetListItems().Result;
            }
        }

        public int AddToItems(CartItem entity)
        {
            //return _cartService.CartActions.AddToCart(entity).Result;
            return _cartService.AddToCart(entity).Result;
        }

        public bool RemoveItem(CartItem entity)
        {
            //return _cartService.CartActions.RemoevFromCart(entity).Result;
            return _cartService.RemoevFromCart(entity).Result;
        }
    }
}
