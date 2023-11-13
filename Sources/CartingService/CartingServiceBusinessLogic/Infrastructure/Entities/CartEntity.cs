using CartingService;
using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceBusinessLogic.Infrastructure.Entities
{
    public class CartEntity : IEntity
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Image { get; set; }

            public int Price { get; set; }

            public int Quantity { get; set; }

        //private string _cartName = string.Empty;
        //private readonly string _collectionName;
        //private readonly string _databaseName;
        //public CartActions<CartItem> Actions;

        //public string CartName  
        //{
        //    get
        //    {
        //        return _cartName;
        //    }
        //    set
        //    {
        //        _cartName = value;
        //    }
        //}

        //public List<CartItem> Items 
        //{
        //    get
        //    {
        //        return Actions.GetListItems().Result;
        //    }
        //}

        //public CartEntity(string databaseName, string collectionName) 
        //{
        //    _collectionName = collectionName;
        //    _databaseName=databaseName;
        //    Actions = new CartActions<CartItem>(_databaseName, _collectionName); 
        //}
    }
}
