using Microsoft.AspNetCore.Mvc;
using PAC6.API.Interfaces;

namespace PAC6.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITesteHttpApplication _testeHttpApplication;

        public TestController(ITesteHttpApplication testeHttpApplication)
        {
            _testeHttpApplication = testeHttpApplication;
        }

        [HttpGet]
        public string Get()
        {
            return "Running";
        }

        [HttpPost]
        public string Post()
        {
            return "Running";
        }

        [HttpPut]
        public string Put()
        {
            return "Running";
        }

        [HttpDelete]
        public string Delete()
        {
            return "Running";
        }

        [HttpPost("TestHttp")]
        public async Task<string> TestHttp()
        {
            return string.Empty;
            //return await _testeHttpApplication.Handle();
        }
    }
}
