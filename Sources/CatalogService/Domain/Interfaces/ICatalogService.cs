using Domain.Entities;


namespace Domain.Interfaces
{
    public interface ICatalogService
    {
        public IActions<Category> CategoryActions { get; }

        public IActionsItem<Item> ItemActions { get; }
    }
}
