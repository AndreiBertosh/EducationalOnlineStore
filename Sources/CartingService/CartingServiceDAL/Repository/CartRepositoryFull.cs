using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;
using LiteDB;

namespace CartingServiceDAL.Repository
{
    public class CartRepositoryFull : IRepository<CartEntity1>
    {
        private readonly string _databaseName;
        private readonly string _collectionName;

        public CartRepositoryFull(string databaseName, string colletion)
        {
            _databaseName = databaseName;
            _collectionName = colletion;
        }

        public Task<int> Add(CartEntity1 item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartEntity1>(_collectionName);
                collection.Insert(item);
            }

            return Task.FromResult(item.Id);
        }

        public Task<bool> Delete(CartEntity1 item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartEntity1>(_collectionName);
                collection.Delete(item.Id);
            }

            return Task.FromResult(true);
        }

        public Task<List<CartEntity1>> GetAll()
        {
            var result = new List<CartEntity1>();
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartEntity1>(_collectionName);
                result = collection.FindAll().ToList();
            }
            return Task.FromResult(result);
        }

        public Task<CartEntity1?> GetById(int id)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartEntity1>(_collectionName);
                return Task.FromResult(collection.Find(e => e.Id == id).FirstOrDefault());
            }
        }

        public Task<CartEntity1> Update(CartEntity1 item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartEntity1>(_collectionName);
                if (item.Id > 0)
                {
                    var cart = collection.Find(c => c.Id == item.Id).FirstOrDefault();
                    cart.Entities = cart.Entities.Union(item.Entities).ToList();
                    collection.Update(cart);
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
