using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoorManagementSystem.API.Controllers
{
    public class AuthController
        : Controller
    {
        private readonly IUsersRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;
        public AuthController(IUsersRepository userRepository, ITokenService tokenService, ISecurityService securityService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _securityService = securityService;
        }
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthRequestDto authRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(authRequest.Email);
        
            if (user == null || !_securityService.VerifyPin(authRequest.Pin, user.PinHash))
            {
                return Unauthorized();
            }
            var roles = await _userRepository.GetUserRolesAsync(user.UserId);


            var token = _tokenService.GenerateJwtToken(user, roles);
            return Ok(new { Token = token });
        }
    }
}
