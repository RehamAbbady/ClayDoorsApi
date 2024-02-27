using DoorManagementSystem.API.Filters;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Authorize]
    [Route("[controller]")]

    public class AccessControlController : Controller
    {
        private readonly IAccessControlService _accessControlService;

        public AccessControlController(IAccessControlService accessControlService)
        {
            _accessControlService = accessControlService;
        }
        [AdminForDoorAuthorization]
        [HttpPost("grant")]
        public async Task<IActionResult> GrantAccess([FromBody] AccessRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var success = await _accessControlService.GrantAccessAsync(request.RoleId, request.DoorId);
            if (success)
                return Ok("Access granted successfully.");
            else
                return BadRequest("Failed to grant access.");
        }
        [AdminForDoorAuthorization]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeAccess([FromBody] AccessRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var success = await _accessControlService.RevokeAccessAsync(request.RoleId, request.DoorId);
            if (success)
                return Ok("Access revoked successfully.");
            else
                return BadRequest("Failed to revoke access.");
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckAccess([FromQuery] AccessCheck request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hasAccess = await _accessControlService.CanAccessDoorAsync(
                request.UserId,
                request.DoorId,
                request.TagCode,
                request.IsRemoteAccessRequested
            );
            return Ok(new { request.UserId, request.DoorId, HasAccess = hasAccess });
        }
    }
}
