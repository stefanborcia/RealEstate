using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ApiDbContext _apiDbContext = new ApiDbContext();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_apiDbContext.Categories);
        }
    }
}
