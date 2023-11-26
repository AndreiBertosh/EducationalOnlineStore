using Asp.Versioning;
using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingServiceWEBAPI.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CartingServiceController : Controller
    {
        private readonly ILogger<CartingServiceController> _logger;
        private readonly ICartActionsNew<CartEntity> _cartActions;

        public CartingServiceController(ILogger<CartingServiceController> logger, ICartProvider cartProvider)
        {
            _logger = logger;
            _cartActions = cartProvider.CartActions;
        }

        /// <summary>
        /// Returns List of CartEntities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCart(string cartName)
        {
            var result = _cartActions.GetCart(cartName).Result;
            return Ok(result);
        }

    }
}
