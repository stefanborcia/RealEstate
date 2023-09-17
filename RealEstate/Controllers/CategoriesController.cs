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

        //Get: api/<CategoriesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_apiDbContext.Categories);
        }

        //Get: api/<CategoriesController>/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           var category = _apiDbContext.Categories.FirstOrDefault(c => c.Id == id);
           return Ok(category);
        }

        //Post: api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            _apiDbContext.Categories.Add(category);
            _apiDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        //Put: api/<CategoriesController>/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category categoryObj)
        {
            var category = _apiDbContext.Categories.Find(id);
            category.Name = categoryObj.Name;
            category.Description = categoryObj.Description;
            category.ImageUrl= categoryObj.ImageUrl;
            _apiDbContext.SaveChanges();
            return Ok("Record updated successfully");
        }

        //Delete: api/<CategoriesController>/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var categoryDeleteById = _apiDbContext.Categories.Find(id);
           _apiDbContext.Categories.Remove(categoryDeleteById);
           _apiDbContext.SaveChanges();
           return Ok("Record deleted");
        }
    }
}
