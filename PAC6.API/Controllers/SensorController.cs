using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAC6.API.Interfaces;
using PAC6.API.Requests;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ICreateSensorApplication _createSensorApplication;

        public SensorController(ICreateSensorApplication createSensorApplication) 
        {
            _createSensorApplication = createSensorApplication;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Create([FromBody] CreateSensorCommand command) => await _createSensorApplication.Handle(command);
        
    }
}
