using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceDAL.Entities;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics;

namespace CartingServiceWEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartingServiceControllerV1 : Controller
    {
        private readonly ILogger<CartingServiceControllerV1> _logger;
        private readonly Cart _cart;

        public CartingServiceControllerV1(ILogger<CartingServiceControllerV1> logger, ICartProvider cartProvider)
        {
            _logger = logger;
            _cart = cartProvider.Cart;
        }

        [HttpGet]
        public Cart GetCart()
        {
            var result = _cart;
            return result;
        }

        [HttpPost]
        public int Add([FromBody] CartEntity value)
        {
            return _cart.AddToItems(value);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var item = _cart.Items.FirstOrDefault(x => x.Id == id);
            if (item == null) 
            {
                return false;
            }
            return _cart.RemoveItem(item);
        }
    }
}
