using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private List<Category> categories = new List<Category>()
        {
            new Category { Id = 1,Name="Apartment", ImageUrl = "apartment.png"},
            new Category { Id = 2,Name="Commercial", ImageUrl = "commercial.png"},
            new Category { Id = 3,Name="House", ImageUrl = "house.png"}
        };
    }
}
