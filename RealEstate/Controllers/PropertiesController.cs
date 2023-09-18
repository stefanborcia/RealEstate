using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private ApiDbContext _dbContext = new ApiDbContext();

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Property property)
        {
            if (property == null)
            {
                return NoContent();
            }
            else
            {
              var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
              var user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
              if (user == null)
              {
                  return NotFound();
                  property.IsTrending = false;
                  property.UserId=user.Id;
                  _dbContext.Properties.Add(property);
                  _dbContext.SaveChanges();
              }
              return StatusCode(StatusCodes.Status201Created);
            }
        }
    }
}
