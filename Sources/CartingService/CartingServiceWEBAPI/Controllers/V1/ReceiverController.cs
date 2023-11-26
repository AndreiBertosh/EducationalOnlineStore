using Asp.Versioning;
using CartingServiceBusinessLogic.Infrastructure.Entities;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.AzureServiceBusreceiver;
using CartingServiceWEBAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingServiceWEBAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReceiverController : Controller
    {
        private readonly IAzureServiceBusReceiver _azureServiceBusReceiver;

        public ReceiverController(ICartProvider cartProvider) 
        {
            _azureServiceBusReceiver =  cartProvider.ServiceBusReceiver;
        }

    [HttpPost]
        public ActionResult SetReseiverState([FromBody] bool value)
        {
            if (value)
            {
                _azureServiceBusReceiver.StartReceive();
                return Ok("receiver was started.");
            }
            else
            {
                _azureServiceBusReceiver.StopReceive();
                return Ok("receiver was stoped.");
            }
        }
    }
}
