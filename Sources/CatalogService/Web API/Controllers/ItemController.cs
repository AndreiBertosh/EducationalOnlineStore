using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly CatalogService catalogService;

        public ItemController()
        {
            catalogService = new CatalogService(string.Empty);
        }

        [HttpGet]
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
        public int Add([FromBody] Item value)
        {
            return catalogService.ItemActions.Add(value).Result;
        }

        [HttpPut("{id}")]
        public bool Update([FromBody] Item value)
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
