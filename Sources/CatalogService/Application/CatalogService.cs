using Application.Actions;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

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
            string connectionString = string.Empty;

            if (string.IsNullOrEmpty(connection))
            {
                connectionString = ConfigurationManager.AppSettings["connectionstring"];
            }
            else 
            {
                connectionString = connection;
            }

            _context = new()
            {
                Connection = connectionString
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