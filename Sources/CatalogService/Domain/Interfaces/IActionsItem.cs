using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IActionsItem<T> : IActions<T>
        where T : IEntity
    {
        Task<bool> DeleteAllItemsForCategoryId(int categoryId);

        Task<List<Item>> GetAllItemsForCategoryId(int categoryId);

        Task<List<Item>> GetItems(int skipItems, int count);
    }
}
