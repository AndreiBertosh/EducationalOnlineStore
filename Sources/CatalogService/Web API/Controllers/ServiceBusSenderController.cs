using Application.AzureServiceBus;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusSenderController : Controller
    {
        private readonly IAzureServiceBusSendService _azureService;

        public ServiceBusSenderController(IAzureServiceBusSendService service)
        {
            _azureService = service;
        }

        [HttpPost]
        public async Task<string> Send([FromBody] string value)
        {
            var result  = await _azureService.Send(value);
            return result;
        }
    }
}
