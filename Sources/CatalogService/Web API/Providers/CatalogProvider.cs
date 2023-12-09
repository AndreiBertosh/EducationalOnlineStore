using Application.AzureServiceBus;
using Domain.Interfaces;
using Domain.Settings;

namespace Web_API.Providers
{
    public class CatalogProvider : ICatalogProvider
    {
        private DatabaseSettings _databaseSettings;
        private QueueSettings _queueSettings;

        public CatalogProvider(ISettingsProvider provider) 
        {
            _databaseSettings = provider.DatabaseSettings;
            _queueSettings = provider.QueueSettings;
        }

        public IAzureServiceBusSendService ServiceBusSender => new AzureServiceBusSendService(_queueSettings.QueueName, _queueSettings.ConnectionString);

    }
}
