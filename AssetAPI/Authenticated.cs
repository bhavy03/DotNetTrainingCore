using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetAPI
{
    public class Authenticated
    {
        private readonly AssetContext _context;
        private readonly IConfiguration _configuration;
        public Authenticated(AssetContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public (string token, string name)? GenerateToken(int id, string Password, string role)
        {
            var user = _context.Customers.SingleOrDefault(u => u.Id == id);

            if (user == null || !PasswordHandler.VerifyPassword(Password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Role, role)]),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"])),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return (tokenString, user.Name);
        }
    }
}

//This method may return either a tuple of two strings (token, name) or it may return null
//So it's a nullable tuple.