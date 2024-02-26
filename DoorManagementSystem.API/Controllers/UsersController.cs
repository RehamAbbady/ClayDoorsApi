using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("/users/")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        
        }
        [HttpGet("/users/{userId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);

        }
        [HttpDelete("remove-role")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId, int roleId)
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
        [HttpPost("{roleId}")]
        public async Task<IActionResult> AddRoleToUser(int userId, int roleId)
        {
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
