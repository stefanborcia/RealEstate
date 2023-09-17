using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        ApiDbContext _apiDbContext = new ApiDbContext();

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
           var userExists = _apiDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
           if (userExists == null)
           {
               return BadRequest("User with same email already exists");
           }
           _apiDbContext.Users.Add(user);
           _apiDbContext.SaveChanges();
           return StatusCode(StatusCodes.Status201Created);
        }
    }
}
