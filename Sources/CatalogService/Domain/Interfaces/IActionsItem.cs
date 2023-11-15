using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IActionsItem<T> : IActions<T>
        where T : IEntity
    {
        public Task<bool> DeleteAllItemsForCategoryId(int categoryId);
    }
}
