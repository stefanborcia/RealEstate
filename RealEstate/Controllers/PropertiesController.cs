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

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Property property)
        {
           var propertyResult = _dbContext.Properties.FirstOrDefault(p => p.Id == id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }

                if (propertyResult.UserId == user.Id)
                {
                    propertyResult.Name = property.Name;
                    propertyResult.Description = property.Description;
                    propertyResult.Price = property.Price;
                    propertyResult.Address = property.Address;
                    property.IsTrending = false;
                    property.UserId = user.Id;
                    _dbContext.Properties.Add(property);
                    _dbContext.SaveChanges();
                    return Ok("Record updated successfully");
                }
                return BadRequest();
            }
        }


    }
}
