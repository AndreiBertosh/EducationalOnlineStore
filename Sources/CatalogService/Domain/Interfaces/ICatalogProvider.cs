namespace Domain.Interfaces
{
    public interface ICatalogProvider
    {
        IAzureServiceBusSendService ServiceBusSender { get; }
    }
}
