using CartingService;
using CartingServiceDAL.Entities;

namespace CartingServiceBusinessLogic.Infrastructure.Entities
{
    public class CartEntity
    {
        private string _cartName;
        private string _collectionName;
        private string _databaseName;
        public CartActions<CartItem> Actions;

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
                return Actions.GetListItems().Result;
            }
        }

        public CartEntity(string databaseName, string collectionName) 
        {
            _collectionName = collectionName;
            _databaseName=databaseName;
            Actions = new CartActions<CartItem>(_databaseName, _collectionName); 
        }
    }
}
