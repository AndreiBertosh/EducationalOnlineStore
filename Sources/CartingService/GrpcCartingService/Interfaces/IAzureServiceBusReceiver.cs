namespace GrpcCartingService.Interfaces
{
    public interface IAzureServiceBusReceiver
    {
        Task StartReceive();

        Task StopReceive();
    }
}
