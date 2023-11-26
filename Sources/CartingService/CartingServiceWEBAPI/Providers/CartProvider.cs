using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Settings;
using CartingServiceWEBAPI.Interfaces;
using CartingServiceWEBAPI.AzureServiceBusreceiver;

namespace CartingServiceWEBAPI.Providers
{
    public class CartProvider : ICartProvider
    {
        private DatabaseSettings _databaseSettings;

        public CartProvider(ISettingsProvider settings)
        {
            _databaseSettings = settings.DatabaseSettings;
        }

        public ICart Cart => new Cart(_databaseSettings.CartName, _databaseSettings.DatabaseName, _databaseSettings.CollectionName);

        public ICartActionsNew<CartEntity> CartActions => new CartActionsNew(_databaseSettings.DatabaseName, _databaseSettings.CollectionName);

        public IAzureServiceBusReceiver ServiceBusReceiver => new AzureServiceBusReceiver(CartActions);
    }
}
