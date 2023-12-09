using Azure.Messaging.ServiceBus;
using Domain.Interfaces;

namespace Application.AzureServiceBus
{
    public class AzureServiceBusSendService : IAzureServiceBusSendService
    {
        private readonly string _queueName; // = "catalogservicequeue";
        private readonly string _connectionString; // = "Endpoint=sb://testbamservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GWMI/c6E6ZLmQzrLhQjL1jgoWG+Gml6Vu+ASbD9xmWc=";

        public AzureServiceBusSendService(string queueName, string connectionString)
        {
            _queueName = queueName;
            _connectionString = connectionString;
        }

        public async Task<string> Send(string message) 
        {
            // name of your Service Bus queue
            // the client that owns the connection and can be used to create senders and receivers
            ServiceBusClient _client;

            // the sender used to publish messages to the queue
            ServiceBusSender _sender;

            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            //_client = new ServiceBusClient(_placeholderdsName, new DefaultAzureCredential(), clientOptions);
            _client = new ServiceBusClient(_connectionString, clientOptions);
            _sender = _client.CreateSender(_queueName);

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();

            messageBatch.TryAddMessage(new ServiceBusMessage(message));

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await _sender.SendMessagesAsync(messageBatch);
                return $"A messages has been updated and published to the queue.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}
