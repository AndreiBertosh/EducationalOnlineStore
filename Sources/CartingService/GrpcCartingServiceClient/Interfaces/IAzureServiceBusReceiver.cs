namespace GrpcCartingServiceClient.Interfaces
{
    public interface IAzureServiceBusReceiver
    {
        Task StartReceive();

        Task StopReceive();
    }
}
