using AssetAPI.Entity;
using AssetAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AssetContext _context;
        private readonly IConfiguration _configuration;
        

        public LoginController(AssetContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("customer/{id}")]
        public IActionResult Login(int id, string Password)
        {
            var authHelper = new Authenticated(_context, _configuration);
            var result = authHelper.GenerateToken(id, Password, "Customer");

            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { result.Value.name, Token = result.Value.token });
            //var user = _context.Customers.SingleOrDefault(u => u.Id == id);

            //if (user == null || !PasswordHandler.VerifyPassword(Password, user.Password))
            //{
            //    return Unauthorized("Invalid credentials");
            //}

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Role, "Customer")]),
            //    Expires = DateTime.UtcNow.AddHours(1),
            //    Issuer = _configuration["JwtConfig:Issuer"],
            //    Audience = _configuration["JwtConfig:Audience"],
            //    SigningCredentials = new SigningCredentials(
            //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"])),
            //        SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);
            //var name = user.Name;
            //return Ok(new { name, Token = tokenString });
        }

        [HttpGet("admin/{id}")]
        public IActionResult AdminLogin(int id, string Password)
        {
            var authHelper = new Authenticated(_context, _configuration);
            var result = authHelper.GenerateToken(id, Password, "Admin");

            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { name = result.Value.name, Token = result.Value.token });
        }
    }
}
