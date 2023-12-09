using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Domain.Interfaces;
using Moq;

namespace WEBAPITests
{
    public class CategoryControllerTests
    {
        private readonly ICatalogService _catalogService;
        private readonly IAzureServiceBusSendService _azureServiceBusSendService;

        public CategoryControllerTests() 
        {
            _azureServiceBusSendService = new Mock<IAzureServiceBusSendService>().Object;
            _catalogService = new CatalogService(string.Empty, _azureServiceBusSendService);
        }
    }
}
