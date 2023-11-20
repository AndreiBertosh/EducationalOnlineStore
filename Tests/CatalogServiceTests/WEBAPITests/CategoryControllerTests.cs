using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Domain.Interfaces;

namespace WEBAPITests
{
    public class CategoryControllerTests
    {
        private readonly ICatalogService _catalogService;

        public CategoryControllerTests() 
        {
            _catalogService = new CatalogService(string.Empty);
        }
    }
}
