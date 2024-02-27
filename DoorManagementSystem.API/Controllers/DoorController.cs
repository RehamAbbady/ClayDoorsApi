using Microsoft.AspNetCore.Mvc;

namespace DoorManagementSystem.API.Controllers
{
    [Route("[controller]")]
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
