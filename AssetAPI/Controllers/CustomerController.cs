using AssetAPI.Entity;
using AssetAPI.Enums;
using AssetAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer,Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly IAssetRepository<Customer> _customerRepository;
        private readonly IAssigningRepository _assigningRepository;
        private readonly AssetContext _context;


        public CustomerController(IAssetRepository<Customer> customerService, IAssigningRepository assigningRepository, AssetContext context)
        {
            _customerRepository = customerService;
            _assigningRepository = assigningRepository;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerRepository.GetAll());
        }

        [HttpGet("search/{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerRepository.Search(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            var newCustomer = _customerRepository.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            var updated = _customerRepository.Update(id, customer);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _customerRepository.Delete(id);
            return success ? Ok("Customer Deleted") : NotFound();
        }

        [HttpPost("requestAsset/{id}")]
        public IActionResult RequestAsset(int id, [FromBody] AssetAssignRequest request)
        {
            var user = _context.Customers.Find(id);
            if (user != null && user.Role == Role.Admin)
            {
                return BadRequest("Admin can't request for asset");
            }
            var result = _assigningRepository.RequestAsset(id, request.assetType, request.AssetId, request.Quantity);
            return result ? Ok("Request Sent") : BadRequest("Request failed.");
        }
    }
}
