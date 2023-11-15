using Asp.Versioning;
using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
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
        private readonly ICart _cart;

        public CartingServiceController(ILogger<CartingServiceController> logger, ICartProvider cartProvider)
        {
            _logger = logger;
            _cart = cartProvider.Cart;
        }

        /// <summary>
        /// Returns Cart object
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCart()
        {
            var result = _cart;
            return Ok(result);
        }

        /// <summary>
        /// Add CartEntity to the Cart
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add([FromBody] CartEntity value)
        {
            var result = _cart.AddToItems(value);
            if (result > 0)
            {
                return Ok(_cart.AddToItems(value));
            }
            return BadRequest("Item was not added.");
        }

        /// <summary>
        /// Remove CartEntity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _cart.Items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return BadRequest("Item was not deleted.");
            }
            return Ok(_cart.RemoveItem(item));
        }
    }
}
