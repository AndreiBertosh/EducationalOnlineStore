using Azure.Core;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcCartingService;
using GrpcCartingService.Interfaces;
using grpc = global::Grpc.Core;

namespace GrpcCartingService.Services
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

        public override Task<HelloReply> SayHelloNew(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public RepeatedField<GRPCCartItem> GetItems(List<GRPCCartItem> items)
        {
            RepeatedField<GRPCCartItem> Items = new();

            Items.AddRange(items);

            return Items;
        }

        public override Task<GetCartResponse> GetCart(GetCartRequest request, ServerCallContext context)
        {
            return Task.FromResult(GetCart(request.CartName));
        }

        public override async Task GetCartServerStream(GetCartRequest request, IServerStreamWriter<GetCartResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("GetCartServerStream proccess started.");
            var cancelationToken = context.CancellationToken;

            await responseStream.WriteAsync(GetCart(request.CartName));
        }

        public override Task<GetCartResponse> AddItem(AddCartItemrequest request, ServerCallContext context)
        {
            _logger.LogInformation("AddItem proccess started.");
            CartEntity value = new CartEntity()
            {
                Name = request.CartName,
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Id = request.Item.Id,
                        Name = request.Item.Name,
                        ImageUrl = request.Item.ImaleUrl,
                        Price = request.Item.Price, 
                        Quantity = request.Item.Quantity,
                    }
                }
            };

            int addResult = _cartActions.AddToCart(value).Result;

            return Task.FromResult(GetCart(request.CartName));

            //throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, "The Item was not added.")); ;
        }

        public override async Task<GetCartResponse> AddItemFromClient(IAsyncStreamReader<AddCartItemrequest> requestStream, ServerCallContext context)
        {
            _logger.LogInformation("AddItemFromClient proccess started.");
            string cartName = string.Empty;

            await foreach (var message in requestStream.ReadAllAsync())
            {
                cartName = message.CartName;
                CartEntity value = new CartEntity()
                {
                    Name = message.CartName,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {   Id = message.Item.Id,
                            Name = message.Item.Name,
                            ImageUrl = message.Item.ImaleUrl,
                            Price = message.Item.Price,
                            Quantity = message.Item.Quantity,
                        }
                    }
                };

                int addResult = _cartActions.AddToCart(value).Result;
                break;
            }

            return GetCart(cartName);
        }

        public override async Task AddItemBothWays(IAsyncStreamReader<AddCartItemrequest> requestStream,
            IServerStreamWriter<GetCartResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("AddItemBothWays proccess started.");

            string cartName = string.Empty;

            await foreach (var message in requestStream.ReadAllAsync())
            {
                cartName = message.CartName;
                CartEntity value = new CartEntity()
                {
                    Name = message.CartName,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Id = message.Item.Id,
                            Name = message.Item.Name,
                            ImageUrl = message.Item.ImaleUrl,
                            Price = message.Item.Price,
                            Quantity = message.Item.Quantity,
                        }
                    }
                };
                int addResult = _cartActions.AddToCart(value).Result;
                break;
            }

            await responseStream.WriteAsync(GetCart(cartName));
        }

        private GetCartResponse GetCart(string cartName)
        {
            _logger.LogInformation("GetCart proccess started.");
            CartEntity result = _cartActions.GetCart(cartName).Result;

            List<GRPCCartItem> cartItems = new();
            cartItems = result.Items.Select(x => new GRPCCartItem
            {
                Id = x.Id,
                Name = x.Name,
                ImaleUrl = x.ImageUrl,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();

            var resultItems = new GetCartResponse
            {
                CartName = cartName
            };
            resultItems.Items.AddRange(cartItems);

            return resultItems;
        }
    }
}