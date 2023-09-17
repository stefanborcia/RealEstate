using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _apiDbContext = new ApiDbContext();

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _apiDbContext.Categories;
        }

        [HttpGet("{id}")]
        public Category GetById(int id)
        {
            return _apiDbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public void Post([FromBody]Category category)
        {
            _apiDbContext.Categories.Add(category);
            _apiDbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category categoryObj)
        {
            var category = _apiDbContext.Categories.Find(id);
            category.Name = categoryObj.Name;
            category.Description = categoryObj.Description;
            category.ImageUrl= categoryObj.ImageUrl;
            _apiDbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           var categoryDeleteById = _apiDbContext.Categories.Find(id);
           _apiDbContext.Categories.Remove(categoryDeleteById);
           _apiDbContext.SaveChanges();

        }
    }
}
