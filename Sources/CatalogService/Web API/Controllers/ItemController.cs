using Application;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly CatalogService catalogService;

        public ItemController(ICatalogProvider provider)
        {
            catalogService = new CatalogService(string.Empty, provider.ServiceBusSender);
        }

        [HttpGet]
        //[RequiredScope("OnlineStore.Read")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
        public IEnumerable<Item> GetAll()
        {
            return catalogService.ItemActions.GetAll().Result;
        }

        [HttpGet("{id}")]
        public Item GetById(int id)
        {
            Item? result = catalogService.ItemActions.GetById(id).Result;

            result ??= new Item();

            return result;
        }

        [HttpPost]
        [RequiredScope("OnlineStore.ReadWrite")]
        public int Add([FromBody] Item value)
        {
            return catalogService.ItemActions.Add(value).Result;
        }

        [HttpPut]
        [RequiredScope("OnlineStore.ReadWrite")]
        public string Update([FromBody] Item value)
        {
            return catalogService.ItemActions.Update(value).Result;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return catalogService.ItemActions.Delete(id).Result;
        }

        [HttpGet("{categoryId}")]
        public IEnumerable<Item> GetAllItemsForCategoryId(int categoryId)
        {
            return catalogService.ItemActions.GetAllItemsForCategoryId(categoryId).Result;
        }

        [HttpGet("{skipItems}/{count}")]
        public IEnumerable<Item> GetItems(int skipItems, int count)
        {
            return catalogService.ItemActions.GetItems(skipItems, count).Result;
        }
    }
}
