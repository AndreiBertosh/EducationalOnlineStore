using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;

namespace GrpcCartingService.Interfaces
{
    public interface ICartProvider
    {
        ICart Cart { get; }

        ICartActionsNew<CartEntity> CartActions { get; }

        public IAzureServiceBusReceiver ServiceBusReceiver { get; }
    }
}
