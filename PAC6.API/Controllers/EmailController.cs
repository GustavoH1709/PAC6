using Microsoft.AspNetCore.Mvc;
using PAC6.API.Interfaces;
using PAC6.API.Requests;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ICreateEmailApplication _createEmailApplication;

        public EmailController(ICreateEmailApplication createEmailApplication)
        {
            _createEmailApplication = createEmailApplication;
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] CreateEmailCommand command) => await _createEmailApplication.Handle(command);

    }
}
