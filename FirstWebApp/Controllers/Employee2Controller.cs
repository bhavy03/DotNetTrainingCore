using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Employee2Controller : ControllerBase
    {
        [HttpGet]
        //[Route("[action]")] 
        //can also use action here
        //[Route("[controller]/[action]")] or this
        public IActionResult Get()
        {
            return Ok("Returning from EmployeeController Get Method");
        }

        [HttpGet]
        public string GetEmployee()
        {
            return "Returning from EmployeeController GetEmployee Method";
        }

        //we can use this instead of common route 

        //can also work if action removed then urls are 
        //api/employee/Emp/All
        ///api/employee/Emp/ById/{Id}

        //if action present then url are
        //api/employee/GetAllEmployees/Emp/All
        //api/employee/GetEmployeeById/Emp/ById/{Id}

        //[Route("Emp/All")]
        //[HttpGet]
        //public string GetAllEmployees()
        //{
        //    return "Response from GetAllEmployees Method";
        //}

        [Route("Emp/ById/{Id}")]
        [HttpGet]
        public string GetEmployeeById(int Id)
        //public string GetEmployeeById([FromRoute(Name = "Id")] int ij) this is another way
        {
            return $"Response from GetEmployeeById Method Id: {Id}";
        }
    }
}

//GET / Emp / Search ? name = Bhavya & age = 21
//[HttpGet("Emp/Search")]
//public string SearchEmployee([FromQuery] string name, [FromQuery] int age)
//{
//    return $"Name: {name}, Age: {age}";
//} or can remove [FromQuery] from this if non ambigous names

//In the ASP.NET Core Application, we can override the Controller level Route Attribute
//at the action method level using the ~(tilde)symbol.
//So, modify the GetAllDepartment action method as follows to use the tilde symbol
//to override the route defined at the employee controller.
//[Route("~/department/all")]
//[HttpGet]
//public string GetAllDepartment()
//{
//    return "Response from GetAllDepartment Method";
//}

//this will give ambigousMatchException
//[Route("{EmployeeId}")]
//[HttpGet]
//public string GetEmployeeDetails(int EmployeeId)
//{
//    return $"Response from GetEmployeeDetails Method, EmployeeId : {EmployeeId}";
//}
//[Route("{EmployeeName}")]
//[HttpGet]
//public string GetEmployeeDetails(string EmployeeName)
//{
//    return $"Response from GetEmployeeDetails Method, EmployeeName : {EmployeeName}";
//}
//We don’t need to specify anything for the string parameter as, by default,
//all the parameters in the ASP.NET Core Web Application are string only.


//[Route("{EmployeeId:int}")] use this to remove ambiguity
//[Route(“{EmployeeName: alpha}”)]  to accept only alphabet (a to z characters) 
//[Route("{EmployeeId:int:max(1000)}")]
//[Route("{EmployeeId:int:min(100):max(1000)}")]
//[Route("{EmployeeId:int:range(100,1000)}")]
//[Route("{EmployeeName:alpha:minlength(5)}")]
//[Route("{EmployeeName:alpha:maxlength(10)}")]
//[Route("{EmployeeName:regex(a(b|c))}")]

//IActionResult is an interface that defines a contract for the result of an action method.
//It allows for the return of any specific result types that implement the IActionResult interface,
//providing maximum flexibility.

//The ProducesResponseType attribute in ASP.NET Core Web API specifies the type of response and
//the HTTP status code that a particular action method can return
//The ProducesResponseType attribute can be applied multiple times to an action method 

//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
////404 Not Found: Indicates that no employees were found.
//[ProducesResponseType(StatusCodes.Status404NotFound)]
////500 Internal Server Error: Indicates an unexpected error during processing.
//[ProducesResponseType(StatusCodes.Status500InternalServerError)]

//public async Task<IActionResult> GetAllEmployees()
//{
//    try
//    {
//        // Simulate an asynchronous operation
//        await Task.Delay(TimeSpan.FromSeconds(1));
//        // Return the list of employees with a 200 OK status
//        return Ok(Employees);
//    }
//    catch (Exception)
//    {
//        // Return 500 Internal Server Error in case of an exception
//        return StatusCode(500, "Internal server error");
//    }
//}

//this is another example that shows the async behaviour
//[ApiController]
//[Route("api/[controller]")]
//public class DemoController : ControllerBase
//{
//    // Async endpoint with artificial delay
//    [HttpGet("slow")]
//    public async Task<IActionResult> GetSlow()
//    {
//        Console.WriteLine($"SLOW Start: {DateTime.Now:T} | Thread: {Thread.CurrentThread.ManagedThreadId}");
//        await Task.Delay(5000); // simulate I/O delay
//        Console.WriteLine($"SLOW End:   {DateTime.Now:T} | Thread: {Thread.CurrentThread.ManagedThreadId}");
//        return Ok("Slow response after 5 seconds");
//    }

//    // Fast endpoint
//    [HttpGet("fast")]
//    public IActionResult GetFast()
//    {
//        Console.WriteLine($"FAST:       {DateTime.Now:T} | Thread: {Thread.CurrentThread.ManagedThreadId}");
//        return Ok("Fast response");
//    }
//}

//When / slow is hit
//It logs SLOW Start, sets a timer for 5 seconds, and frees the thread
//The thread is returned to the thread pool, so the app stays responsive
//When /fast is hit
//It immediately gets a thread from the pool and executes
//After 5 seconds, the framework resumes the slow action on another thread and logs SLOW End.

//OKResult is inherited from the StatusCodeResult class
//The StatusCodeResult class is inherited from ActionResult, and we have already discussed that
//the ActionResult class is inherited from the IActionResult interface.

//return StatusCode(200); // Manually setting 200 OK without Data
//return StatusCode(200, listEmployees); // Manually setting 200 OK with Data