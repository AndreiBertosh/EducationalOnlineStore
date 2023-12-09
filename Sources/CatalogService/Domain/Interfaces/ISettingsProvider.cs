using Domain.Settings;

namespace Domain.Interfaces
{
    public interface ISettingsProvider
    {
        public DatabaseSettings DatabaseSettings { get; }

        public QueueSettings QueueSettings { get; }

    }
}
