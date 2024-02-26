using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Route("api/doors")]
    [ApiController]
    public class DoorController : Controller
    { 
        private IConfiguration _configuration;
        public DoorController(IConfiguration configuration)
        {
            _configuration=configuration ;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var l=_configuration.GetConnectionString("DoorManagementDatabase");
            return View();
        }
    }
}
