using CartingService;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceBusinessLogic
{
    public class CartService
    {
        public CartEntity _cartEntity;

        public CartActions<CartItem> CartActions;

        public CartService(string databaseName, string cartName)
        {
            this.CartActions = new CartActions<CartItem>(databaseName, cartName);

            var item = new CartItem();

            _cartEntity = new CartEntity(databaseName, cartName);
            _cartEntity.Actions.AddToCart(item);
        }
    }
}
