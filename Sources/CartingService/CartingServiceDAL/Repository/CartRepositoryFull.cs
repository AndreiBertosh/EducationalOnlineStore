using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;
using LiteDB;

namespace CartingServiceDAL.Repository
{
    public class CartRepositoryFull : IRepository<CartModel>
    {
        private readonly string _databaseName;
        private readonly string _collectionName;

        public CartRepositoryFull(string databaseName, string colletion)
        {
            _databaseName = databaseName;
            _collectionName = colletion;
        }

        public Task<int> Add(CartModel item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                if (item.Id == 0)
                {
                    collection.Insert(item);
                }
            }

            return Task.FromResult(item.Id);
        }

        public Task<bool> Delete(CartModel item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                collection.Delete(item.Id);
            }

            return Task.FromResult(true);
        }

        public Task<List<CartModel>> GetAll()
        {
            var result = new List<CartModel>();
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                result = collection.FindAll().ToList();
            }
            return Task.FromResult(result);
        }

        public Task<CartModel?> GetById(int id)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                return Task.FromResult(collection.Find(e => e.Id == id).FirstOrDefault());
            }
        }

        public Task<CartModel> Update(CartModel item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                if (item.Id > 0)
                {
                    var cartCollection = collection.Find(c => c.Id == item.Id).FirstOrDefault();
                    cartCollection.Items = cartCollection.Items.Union(item.Items).ToList();
                    collection.Update(cartCollection);
                }
                else
                {
                    collection.Insert(item);
                }
            }
            return Task.FromResult(item);
        }
    }
}
