using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Settings;
using GrpcCartingService.Interfaces;
using GrpcCartingService.AzureServiceBusreceiver;

namespace GrpcCartingService.Providers
{
    public class CartProvider : ICartProvider
    {
        private DatabaseSettings _databaseSettings;
        private QueueSettings _queueSettings;

        public CartProvider(ISettingsProvider settings)
        {
            _databaseSettings = settings.DatabaseSettings;
            _queueSettings = settings.QueueSettings;
        }

        public ICart Cart => new Cart(_databaseSettings.CartName, _databaseSettings.DatabaseName, _databaseSettings.CollectionName);

        public ICartActionsNew<CartEntity> CartActions => new CartActionsNew(_databaseSettings.DatabaseName, _databaseSettings.CollectionName);

        public IAzureServiceBusReceiver ServiceBusReceiver => new AzureServiceBusReceiver(CartActions, _queueSettings);
    }
}
