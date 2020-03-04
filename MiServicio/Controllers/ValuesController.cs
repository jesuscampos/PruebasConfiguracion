using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace MiServicio.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ValuesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Config()
        {
            return new List<string>
            {
                configuration.GetSection("Audit").GetValue<string>("ConnectionString"),
                configuration.GetValue<string>("InstrumentationKey")
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Env()
        {
            return new List<string>
            {
                System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                System.Environment.GetEnvironmentVariable("Audit:ConnectionString"),
                System.Environment.GetEnvironmentVariable("InstrumentationKey")
            };
        }
    }
}
