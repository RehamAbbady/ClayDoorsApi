using DoorManagementSystem.API.Filters;
using DoorManagementSystem.API.Models;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GrantAccess([Required][FromBody] AccessRequestDto request)
        {
            if (request==null ||!ModelState.IsValid)
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
        public async Task<IActionResult> RevokeAccess([Required][FromBody] AccessRequestDto request)
        {
            if (request == null || !ModelState.IsValid)
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
        public async Task<IActionResult> CheckAccess([Required][FromBody] AccessCheck request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hasAccess = await _accessControlService.CanAccessDoorAsync(
                request.UserId,
                request.DoorId,
                request.TagCode,
                request.IsRemoteAccessRequested
            );
            var checkAccessResult = new CheckAccessResult()
            {
                UserId = request.UserId,
                DoorId = request.DoorId,
                HasAccess = hasAccess
            };
            return Ok(checkAccessResult);
        }
    }
}
