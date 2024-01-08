using Azure.Core;
using Azure.Messaging.ServiceBus;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceBusinessLogic.Infrastructure.Settings;
using GrpcCartingServiceClient.Interfaces;
using System.Text.Json;

namespace GrpcCartingServiceClient.AzureServiceBusreceiver
{
    public class AzureServiceBusReceiver : IAzureServiceBusReceiver, IDisposable
    {
        private readonly string _queueName; // = "catalogservicequeue/$deadletterqueue"; 
        private readonly string _connectionString; // = "Endpoint=sb://testbamservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GWMI/c6E6ZLmQzrLhQjL1jgoWG+Gml6Vu+ASbD9xmWc=";
        private readonly ICartActionsNew<CartEntity> _cartActions;

        // the client that owns the connection and can be used to create senders and receivers
        private readonly ServiceBusClient client;

        // the processor that reads and processes messages from the queue
        private readonly ServiceBusProcessor processor;
        private readonly ServiceBusReceiver receiver;

        public AzureServiceBusReceiver(ICartActionsNew<CartEntity> cartActions, QueueSettings settings)
        {
            _cartActions = cartActions;
            _queueName = settings.QueueName;
            _connectionString = settings.ConnectionString;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets,
            };
            clientOptions.RetryOptions.TryTimeout = new TimeSpan(0, 0, 5);

            client = new ServiceBusClient(_connectionString, clientOptions);
            processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            receiver = client.CreateReceiver(_queueName, new ServiceBusReceiverOptions());

        }

        public async Task StartReceive()
        {
            // add handler to process messages
            processor.ProcessMessageAsync += MessageHandler;
            // add handler to process any errors
            processor.ProcessErrorAsync += ErrorHandler;
            // start processing 
            await processor.StartProcessingAsync();
        }

        public async Task StopReceive()
        {
            // Stop processing
            await processor.StopProcessingAsync();

        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            try
            {
                CartItem? item = JsonSerializer.Deserialize<CartItem>(body);
                if (item != null)
                {
                    bool result = await _cartActions.ItemsUpdate(item);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                // complete the message. message is deleted from the queue. 
                await args.CompleteMessageAsync(args.Message);
            }
        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async void Dispose()
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}
