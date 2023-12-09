using LiteDB;
using CartingServiceDAL.Infrastructure.Interfaces;

namespace CartingServiceDAL.Repository
{
    public class CartRepository<T> : IRepository<T> 
        where T : IEntity
    {
        private readonly string _databaseName;
        private readonly string _collectionName;

        public CartRepository(string databaseName, string colletion)
        {
            _databaseName = databaseName;
            _collectionName = colletion;
        }

        public Task<int> Add(T item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<T>(_collectionName);
                collection.Insert(item);
            }

            return Task.FromResult(item.Id);
        }

        public Task<bool> Delete(T item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<T>(_collectionName);
                collection.Delete(item.Id);
            }

            return Task.FromResult(true);
        }

        public Task<List<T>> GetAll()
        {
            var result = new List<T>();
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<T>(_collectionName);
                result = collection.FindAll().ToList();
            }
            return Task.FromResult(result);
        }

        public Task<T?> GetById(int id)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<T>(_collectionName);
                return Task.FromResult(collection.Find(e => e.Id == id).FirstOrDefault());
            }
        }

        public Task<T> Update(T item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<T>(_collectionName);
                collection.Update(item);
            }
            return Task.FromResult(item);
        }
    }
}
