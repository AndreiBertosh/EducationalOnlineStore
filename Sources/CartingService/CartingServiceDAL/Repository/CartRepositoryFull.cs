using CartingServiceDAL.Entities;
using CartingServiceDAL.Infrastructure.Interfaces;
using LiteDB;
using System.Linq;

namespace CartingServiceDAL.Repository
{
    public class CartRepositoryFull : IRepositoryFull<CartModel>
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

        public Task<bool> Delete(string cartName, int itemId)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                var result = collection.Find(c => c.Name == cartName).FirstOrDefault();
                if (result != null)
                {
                    var removeItems = result.Items.Find(i => i.Id == itemId);
                    if (removeItems != null)
                    {
                        result.Items.Remove(removeItems);
                        collection.Update(result);
                    }
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public Task<CartModel?> GetAll(string cartName)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                return Task.FromResult(collection.Find(c => c.Name == cartName).FirstOrDefault());
            }
        }

        public Task<CartModel?> GetById(int id)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                return Task.FromResult(collection.Find(e => e.Id == id).FirstOrDefault());
            }
        }

        public Task<CartModel> Update(CartModel model)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                var cart = collection.Find(c => c.Name == model.Name).FirstOrDefault();

                if (cart != null)
                {
                    cart.Items = cart.Items.UnionBy(model.Items, c => c.Id).OrderBy(c => c.Id).ToList();
                    int id = cart.Items.MaxBy(x => x.Id).Id;
                    foreach (var item in cart.Items.Where(x => x.Id == 0).ToList())
                    {
                        item.Id = id + 1;
                        id++;
                    }
                    collection.Update(cart);
                }
                else
                {
                    collection.Insert(model);
                }
            }
            return Task.FromResult(model);
        }

        public Task<bool> ItemsUpdate(CartItemModel item)
        {
            using (var database = new LiteDatabase(_databaseName))
            {
                var collection = database.GetCollection<CartModel>(_collectionName);
                var carts = collection.FindAll();
                
                foreach (var cart in carts.Where(c => c.Id == item.Id)) 
                {
                    var items = cart.Items
                        .Where(c => c.Id == item.Id)
                        .Select(c => new CartItemModel
                        {
                            Id = c.Id,
                            Name = item.Name,
                            ImageUrl = item.ImageUrl,
                            Price = item.Price,
                            Quantity = c.Quantity
                        }).ToList();
                    cart.Items.RemoveAll(c => c.Id == item.Id);
                    cart.Items.AddRange(items);
                    cart.Items = cart.Items.OrderBy(c => c.Id).ToList();
                    collection.Update(cart);
                }
                return Task.FromResult(true);
            }
        }
    }
}
