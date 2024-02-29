using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoorManagementSystem.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserDto user)
        {
            var claims = new List<Claim>
        {
            new Claim("user_id", user.UserId.ToString())

        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
