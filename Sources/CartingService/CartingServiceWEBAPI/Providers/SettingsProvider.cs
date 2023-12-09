using CartingServiceBusinessLogic.Infrastructure.Settings;
using CartingServiceWEBAPI.Interfaces;

namespace CartingServiceWEBAPI.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfiguration _configuration;

        public SettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DatabaseSettings DatabaseSettings => _configuration
            .GetSection("DatabaseSettings")
            .Get<DatabaseSettings>() ?? new DatabaseSettings();

        public QueueSettings QueueSettings => _configuration
            .GetSection("QueueSettings")
            .Get<QueueSettings>() ?? new QueueSettings();
    }
}
