using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Route("api/door-logs")]

    public class DoorLogsController : Controller
    {
        private IDoorLogsService _doorLogsService;
        public DoorLogsController(IDoorLogsService doorLogsService)
        {
            _doorLogsService = doorLogsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccessLogs([FromQuery] int? userId, [FromQuery] int? doorId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] bool? isSuccess)
        {
            var logs = await _doorLogsService.GetAccessLogsAsync(userId, doorId, startDate, endDate, isSuccess);
            return Ok(logs);
        }
    }
}
