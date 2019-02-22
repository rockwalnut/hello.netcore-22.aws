using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace hello.netcore_22.aws.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Hello World with .NETCore Web API at " + DateTime.Now;
        }

        
    }
}