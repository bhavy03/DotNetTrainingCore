using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWeb.Controllers
{
    [ApiController]
    [Route("test")]
    //[Route("test/{action}")]
    //if we do this and then in url provide localhost:5043/test/get2 then it will work
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Returning from TestController Get Method";
        }

        //public string Gedt2()
        //{
        //    return "Returning from TestController Get2 Method";
        //}
        //adding another get method gives http error 500
    }
}

//IActionResult is an interface in ASP.NET Core that represents the result of an action method in a controller.
//[HttpGet]
//public IActionResult GetUser()
//{
//    var user = new { Id = 1, Name = "Bhavya" };
//    return Ok(user); // returns 200 with user info
//}
