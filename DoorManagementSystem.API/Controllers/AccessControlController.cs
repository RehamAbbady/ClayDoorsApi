﻿using DoorManagementSystem.API.Models;
using DoorManagementSystem.Application.DTOs;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Entities;
using DoorManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DoorManagementSystem.API.Controllers
{
    //  [Authorize]
    [Route("[controller]")]

    public class AccessControlController : Controller
    {
        private readonly IAccessControlService _accessControlService;
        private readonly IRolePermissionService _rolePermissionService;

        public AccessControlController(IAccessControlService accessControlService, IRolePermissionService rolePermissionService)
        {
            _accessControlService = accessControlService;
            _rolePermissionService = rolePermissionService;
        }
        [HttpPost("grant")]
        public async Task<IActionResult> GrantAccess([FromBody]AccessRequestDto request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userClaims = User.Claims;

            bool requestUserUasAccess = await _accessControlService.AuthorizeRequestUserPermissionAsync(userClaims, request.DoorId, request.RequestedPermission);
            if (!requestUserUasAccess)
            {
                return Unauthorized("unauothorized action");
            }
            var grantAccesssResult = await _accessControlService.GrantAccessAsync(request.DoorId, request.RoleId, request.RequestedPermission, request.UserId);
            if (grantAccesssResult.Key)
                return Ok(grantAccesssResult.Value);
            else
                return BadRequest(grantAccesssResult.Value);
        }
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeAccess([FromBody] AccessRequestDto request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userClaims = User.Claims;

            bool requestUserUasAccess = await _accessControlService.AuthorizeRequestUserPermissionAsync(userClaims, request.DoorId, request.RequestedPermission);
            if (!requestUserUasAccess)
            {
                return Unauthorized("unauothorized action");
            }

            var revokeAccesssResult = await _accessControlService.RevokeAccessAsync(request.DoorId, request.RoleId, request.RequestedPermission, request.UserId);
            if (revokeAccesssResult.Key)
                return Ok(revokeAccesssResult.Value);
            else
                return BadRequest(revokeAccesssResult.Value);
        }

        [HttpGet("users/{userId}/doors/{doorId}/check")]
        public async Task<IActionResult> CheckAccess([FromRoute] int userId, [FromRoute] int doorId, [Required][FromQuery] string tagCode, [FromQuery] bool isRemote = false)
        {
            if (userId <= 0 || doorId <= 0 || tagCode.Length != 12)
                return BadRequest("invalid input");


            var hasAccess = await _accessControlService.CanOpenDoorAsync(
                userId,
                doorId,
                tagCode,
                isRemote
            );
            var checkAccessResult = new CheckAccessResult()
            {
                UserId = userId,
                DoorId = doorId,
                HasAccess = hasAccess
            };
            return Ok(checkAccessResult);
        }
    }
}
