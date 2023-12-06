using Microsoft.AspNetCore.Mvc;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet] 
        public string Index()
        {
            return "API Online";
        }
    }
}
