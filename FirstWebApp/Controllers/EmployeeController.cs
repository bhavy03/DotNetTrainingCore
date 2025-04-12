using FirstWebApp.Models;
using FirstWebApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Text;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        //Injects IEmployeeRepository to manage data operations.
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;   //Assigning pizza shop to delivery boy
        }
        //Services registered in Program.cs.
        //HTTP request hits a controller route.
        //DI container resolves dependencies by:
        //Matching interfaces to implementations.
        //Creating instances if needed.
        //Injecting them via the constructor.
        //Constructor is called automatically with injected object.
        //Your controller runs with everything ready to go.
        //Retrieves all employees (GET api/employee).


        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _repository.GetAll();
            return Ok(employees);
        }

        //Retrieves a specific employee by ID(GET api/employee/{id}).
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //Adds a new employee(POST api/employee).
        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // Updates an existing employee entirely (PUT api/employee/{id}).
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }
            if (!_repository.Exists(id))
            {
                return NotFound();
            }
            _repository.Update(employee);
            return NoContent();
        }

        // Partially updates an existing employee (PATCH api/employee/{id}).
        [HttpPatch("{id}")]
        public IActionResult PatchEmployee(int id, [FromBody] Employee employee)
        {
            var existingEmployee = _repository.GetById(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            // For simplicity, updating all fields. In real scenarios, use JSON Patch.
            existingEmployee.Name = employee.Name ?? existingEmployee.Name;
            existingEmployee.Position = employee.Position ?? existingEmployee.Position;
            existingEmployee.Age = employee.Age != 0 ? employee.Age : existingEmployee.Age;
            existingEmployee.Email = employee.Email ?? existingEmployee.Email;
            _repository.Update(existingEmployee);
            return NoContent();
        }

        // Deletes an employee (DELETE api/employee/{id}).
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_repository.Exists(id))
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
    }
}


//[HttpGet("Search")]
//Optional Parameters: The ? in string? indicates that the parameters are optional.
//public ActionResult<IEnumerable<Employee>> SearchEmployees([FromQuery] string? gender,
//[FromQuery] string? department, [FromQuery] string? city)

//if multiple parameters then simply create the class and then use its object
//public class EmployeeSearch
//{
//    public string? Gender { get; set; }
//    public string? Department { get; set; }
//    public string? City { get; set; }
//}
//[HttpGet("Search")]
//public ActionResult<IEnumerable<Employee>> SearchEmployees([FromQuery] EmployeeSearch searchCriteria)
//or we can simply use this
//var gender = HttpContext.Request.Query["Gender"].ToString();

//this is combined
// GET api/Employee/Gender/Male?Department=IT&City=Los Angeles
//[HttpGet("Gender/{gender}")]
//public ActionResult<IEnumerable<Employee>> GetEmployeesByGender([FromRoute] string gender,
//[FromQuery] string? department, [FromQuery] string? city)
//Route parameters are part of the URL path, while query strings are appended to the URL.

//[HttpGet("All")]
//[HttpGet("AllEmployees")]
//[HttpGet("GetAll")]
//Action method
//[Route("api/old-employees")]
//[Route("api/staff")]
//[HttpGet]
//public ActionResult<IEnumerable<Employee>> GetAllEmployees()
//{
//    var employees = EmployeeData.Employees;
//    return Ok(employees);
//}
//[HttpGet("All")]	Shorthand for [HttpGet] + [Route("All")