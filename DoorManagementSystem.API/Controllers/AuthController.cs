﻿using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GenerateToken([FromBody] AuthRequestDto authRequest)
        {
            var user = await _userService.GetUserDetailsByEmailAsync(authRequest.Email);

            if (user == null || !_securityService.VerifyPin(authRequest.Pin, user.PinHash))
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
