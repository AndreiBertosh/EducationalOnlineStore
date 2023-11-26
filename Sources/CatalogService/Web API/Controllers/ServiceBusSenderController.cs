using Application.AzureServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusSenderController : Controller
    {
        private readonly AzureServiceBusSendService _azureService;

        public ServiceBusSenderController()
        {
            _azureService = new AzureServiceBusSendService();
        }

        [HttpPost]
        public async Task<string> Send([FromBody] string value)
        {
            var result  = await _azureService.Send(value);
            return result;
        }
    }
}
