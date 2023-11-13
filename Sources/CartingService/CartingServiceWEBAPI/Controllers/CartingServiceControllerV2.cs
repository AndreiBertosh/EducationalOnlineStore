using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingServiceWEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartingServiceControllerV2 : Controller
    {
        private readonly ILogger<CartingServiceControllerV1> _logger;
        private readonly Cart _cart;

        public CartingServiceControllerV2(ILogger<CartingServiceControllerV1> logger, ICartProvider cartProvider)
        {
            _logger = logger;
            _cart = cartProvider.Cart;
        }

        [HttpGet]
        public IEnumerable<CartEntity> GetItems()
        {
            var result = _cart.Items;
            return result;
        }
    }
}
