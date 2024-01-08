using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Interfaces;
using Grpc.Core;

namespace CartingServiceWEBAPI.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ICartActionsNew<CartEntity> _cartActions;

        public GreeterService(ILogger<GreeterService> logger, ICartProvider cartProvider)
        {
            _logger = logger;
            _cartActions = cartProvider.CartActions;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public Task<CartEntity> GetCart(GetCartRequest request, ServerCallContext context)
        {
            return _cartActions.GetCart(request.CartName);
        }
    }
}