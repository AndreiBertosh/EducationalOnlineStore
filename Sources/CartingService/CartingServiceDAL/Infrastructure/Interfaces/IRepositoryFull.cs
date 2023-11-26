using CartingServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingServiceDAL.Infrastructure.Interfaces
{
    public interface IRepositoryFull<T>
        where T : IEntity
    {
        Task<int> Add(T item);

        Task<bool> Delete(string cartName, int itemId);

        Task<T?> GetById(int id);

        Task<T?> GetAll(string cartName);

        Task<T> Update(T item);

        Task<bool> ItemsUpdate(CartItemModel item);
    }
}
