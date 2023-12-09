using CartingServiceBusinessLogic.Infrastructure.Settings;

namespace CartingServiceWEBAPI.Interfaces
{
    public interface ISettingsProvider
    {
        public DatabaseSettings DatabaseSettings { get; }

        public QueueSettings QueueSettings { get; }

    }
}
