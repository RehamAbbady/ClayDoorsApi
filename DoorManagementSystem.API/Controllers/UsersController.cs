using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

 
        [HttpGet("/users/{userId}/doors/{doorId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetById(int userId,int doorId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);

        }
        [HttpDelete("/users/{userId}/doors/{doorId}/remove-role")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId, int doorId, int roleId)
        {
            var success = await _userService.RemoveRoleFromUserAsync(userId, roleId);
            if (success)
            {
                return Ok("Role removed from user successfully.");
            }
            else
            {
                return NotFound("Role or user not found.");
            }
        }
        [HttpPost("users/{userId}/doors/{doorId}/add-role")]
        public async Task<IActionResult> AddRoleToUser(int userId,int doorId , [Required][FromBody] int roleId)
        {
            if (userId <= 0 || doorId <= 0|| roleId<=0)
            {
                return BadRequest("User ID, Door ID, Role ID must be positive numbers.");
            }
            var success = await _userService.AddRoleToUserAsync(userId, roleId);
            if (success)
            {
                return Ok("Role added to user successfully.");
            }
            else
            {
                return BadRequest("Failed to add role to user, or role already exists.");
            }
        }
    }
}
