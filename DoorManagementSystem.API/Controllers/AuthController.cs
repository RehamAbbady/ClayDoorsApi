using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.API.Controllers
{
    [Route("[controller]")]

    public class AuthController
        : Controller
    {
        private readonly IUsersService _userService;
        private readonly ITokenService _tokenService;
        private readonly ISecurityService _securityService;
        public AuthController(IUsersService userService, ITokenService tokenService, ISecurityService securityService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _securityService = securityService;
        }
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([Required][FromBody] AuthRequestDto authRequest)
        {
            if (authRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUserDetailsByEmailAsync(authRequest.Email);

            if (user == null || !_securityService.VerifyPin(authRequest.Pin, user.PinHash))
            {
                return Unauthorized("wrong pin or email");
            }

            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
