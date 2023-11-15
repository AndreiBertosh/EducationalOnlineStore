using Application.Actions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using System.Configuration;

namespace Application
{
    public class CatalogService : ICatalogService
    {
        private InfrastructureContext _context;

        private CategoryRepository _categoryRepository;
        private ItemRepository _itemRepository;

        private CategoryActions _categoryActions;
        private ItemActions _itemActions;

        public CatalogService(string? connection) 
        {
            string connectionString = string.Empty;

            if (string.IsNullOrEmpty(connection))
            {
                connectionString = ConfigurationManager.ConnectionStrings["CatalogService"].ConnectionString;
            }
            else 
            {
                connectionString = connection;
            }

            _context = new()
            {
                Connection = connectionString
            };

            _itemRepository = new(_context);
            _categoryRepository = new(_context);
            _itemActions = new(_itemRepository);
            _categoryActions = new(_categoryRepository, _itemActions);
        }

        public IActions<Category> CategoryActions
        { 
            get 
            { 
                return _categoryActions; 
            } 
        }

        public IActionsItem<Item> ItemActions 
        { 
            get
            {
                return _itemActions; 
            } 
        }
    }
}
