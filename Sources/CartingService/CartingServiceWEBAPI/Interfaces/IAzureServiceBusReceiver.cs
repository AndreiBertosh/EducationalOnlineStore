namespace CartingServiceWEBAPI.Interfaces
{
    public interface IAzureServiceBusReceiver
    {
        Task StartReceive();

        Task StopReceive();
    }
}
