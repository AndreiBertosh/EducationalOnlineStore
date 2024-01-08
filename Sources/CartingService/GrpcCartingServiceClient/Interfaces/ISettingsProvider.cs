﻿using CartingServiceBusinessLogic.Infrastructure.Settings;

namespace GrpcCartingServiceClient.Interfaces
{
    public interface ISettingsProvider
    {
        public DatabaseSettings DatabaseSettings { get; }

        public QueueSettings QueueSettings { get; }
    }
}
