using Asp.Versioning;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Infrastructure.Requests;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingServiceWEBAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
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
        /// Returns Cart object
        /// </summary>
        /// <param name="cartName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCart(string cartName)
        {
            var result = _cartActions.GetCart(cartName).Result;

            return Ok(result.Items);
        }

        /// <summary>
        /// Add CartEntity to the Cart
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add([FromBody] CartEntity value)
        {
            var result = _cartActions.AddToCart(value).Result;
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest("Item was not added.");
        }

        /// <summary>
        /// Remove CartEntity from the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete()]
        public ActionResult Delete([FromBody] DeleteRequest request)
        {
            var cartName = request.Name;
            var result = _cartActions.RemoevFromCart(request.Name, request.cartItemId).Result;
            if (!result)
            {
                return BadRequest("Item was not deleted.");
            }
            return Ok(result);
        }
    }
}
