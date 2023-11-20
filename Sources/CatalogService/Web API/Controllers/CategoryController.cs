using Application;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatalogService catalogService;

        public CategoryController() 
        {
            catalogService = new CatalogService(string.Empty);
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return catalogService.CategoryActions.GetAll().Result;
        }

        [HttpGet("{id}")]
        public Category GetById(int id)
        {
            Category? result = catalogService.CategoryActions.GetById(id).Result;

            result ??= new Category();

            return  result;
        }

        [HttpPost]
        public int Add([FromBody] Category value)
        {
            return catalogService.CategoryActions.Add(value).Result;
        }

        [HttpPut("{id}")]
        public bool Update([FromBody] Category value)
        {
            return catalogService.CategoryActions.Update(value).Result;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            catalogService.ItemActions.DeleteAllItemsForCategoryId(id);
            return catalogService.CategoryActions.Delete(id).Result;
        }
    }
}
