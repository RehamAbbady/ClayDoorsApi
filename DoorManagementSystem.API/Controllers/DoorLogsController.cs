using DoorManagementSystem.API.Filters;
using DoorManagementSystem.API.Models;
using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Route("api/door-logs")]
    [Authorize]
    [AdminForDoorAuthorization]

    public class DoorLogsController : Controller
    {
        private IDoorLogsService _doorLogsService;
        public DoorLogsController(IDoorLogsService doorLogsService)
        {
            _doorLogsService = doorLogsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccessLogs([FromQuery] AccessLogQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var logs = await _doorLogsService.GetAccessLogsAsync(query.UserId,query.DoorId , query.StartDate, query.EndDate, query.IsSuccess);
            return Ok(logs);
        }
    }
}
