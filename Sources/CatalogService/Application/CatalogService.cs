using Domain.Actions;
using Infrastructure;
using Infrastructure.Repositories;

namespace Application
{
    public class CatalogService
    {
        private InfrastructureContext _context;

        private CategoryRepository _categoryRepository;
        private ItemRepository _itemRepository;

        private CategoryActions _categoryActions;
        private ItemActions _itemActions;

        public CatalogService(string? connection) 
        {
            string _connection = string.Empty;

            if (string.IsNullOrEmpty(connection))
            {
                _connection = AppConfig.ConnectionString;
            }
            else 
            {
                _connection = connection;
            }

            _context = new()
            {
                Connection = _connection
            };

            _categoryRepository = new(_context);
            _itemRepository = new(_context);
            _categoryActions = new(_categoryRepository);
            _itemActions = new(_itemRepository);
        }

        public CategoryActions CategoryActions 
        { 
            get 
            { 
                return _categoryActions; 
            } 
        }

        public ItemActions ItemActions 
        { 
            get
            {
                return _itemActions; 
            } 
        }
    }
}