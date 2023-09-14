using Microsoft.AspNetCore.Mvc;
using PAC6.API.Interfaces;
using PAC6.API.Requests;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly ICreateParametersApplication _createParametersApplication;

        public ParametersController(ICreateParametersApplication createParametersApplication)
        {
            _createParametersApplication = createParametersApplication;
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] CreateParametersCommand commad) => await _createParametersApplication.Handle(commad);
    }
}
