using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        readonly ApiDbContext _apiDbContext = new ApiDbContext();
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentUser =
                _apiDbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (currentUser == null)
            {
                return NotFound("User is not found");
            }

           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
           var credentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           var claims = new[]
           {
               new Claim(ClaimTypes.Email, user.Email)
           };
           var token = new JwtSecurityToken(
               issuer: _configuration["JWT:Issuer"],
               audience: _configuration["JWT: Audience"],
               claims: claims,
               expires:DateTime.Now.AddMinutes(60),
               signingCredentials:credentials);
           var jwt = new JwtSecurityTokenHandler().WriteToken(token);
           return Ok(jwt);
        }
    }
}
