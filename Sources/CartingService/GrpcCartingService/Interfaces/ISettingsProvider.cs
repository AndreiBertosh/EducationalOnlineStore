using CartingServiceBusinessLogic.Infrastructure.Settings;

namespace GrpcCartingService.Interfaces
{
    public interface ISettingsProvider
    {
        public DatabaseSettings DatabaseSettings { get; }

        public QueueSettings QueueSettings { get; }
    }
}
