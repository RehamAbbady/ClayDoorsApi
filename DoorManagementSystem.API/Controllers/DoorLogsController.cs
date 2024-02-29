using DoorManagementSystem.API.Models;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Route("/door-logs")]
   // [Authorize]

    public class DoorLogsController : Controller
    {
        private readonly IDoorLogsService _doorLogsService;
        private readonly IAccessControlService _accessControlService;
        public DoorLogsController(IDoorLogsService doorLogsService, IAccessControlService accessControlService)
        {
            _doorLogsService = doorLogsService;
            _accessControlService = accessControlService;
        }
        [HttpGet("/doors/{doorId}")]
        public async Task<IActionResult> GetAccessLogs(int doorId,[FromQuery] AccessLogQuery query)
        {
            if ((query.UserId!=null&&query.UserId<=0)||doorId<=0||!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var claimsPrinciple = User;
            bool requestUserUasAccess = await _accessControlService.AuthorizeRequestUserPermissionAsync(claimsPrinciple,doorId, Permissions.ViewLogs);
            if (!requestUserUasAccess)
            {
                return Unauthorized("unauothorized action");
            }
            var logs = await _doorLogsService.GetAccessLogsAsync(query.UserId, doorId, query.StartDate, query.EndDate, query.IsSuccess);
            return Ok(logs);
        }
    }
}
