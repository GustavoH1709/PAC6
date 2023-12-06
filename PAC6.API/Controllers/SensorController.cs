using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Requests;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SensorController : ControllerBase
    {
        private readonly ICreateSensorApplication _createSensorApplication;
        private readonly IGetSensorApplication _getSensorApplication;

        public SensorController(ICreateSensorApplication createSensorApplication, IGetSensorApplication getSensorApplication)
        {
            _createSensorApplication = createSensorApplication;
            _getSensorApplication = getSensorApplication;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> Create([FromQuery] CreateSensorCommand command) => await _createSensorApplication.Handle(command);

        [HttpGet("gravados")]
        [AllowAnonymous]
        public async Task<List<DataDTO>> Get() => await _getSensorApplication.Handle();
        
    }
}
