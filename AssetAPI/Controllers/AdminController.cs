using AssetAPI.Enums;
using AssetAPI.Repositories;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AssetContext _context;
        private readonly IAssigningRepository _assigningRepository;
        public AdminController(AssetContext context, IAssigningRepository assigningRepository)
        {
            _context = context;
            _assigningRepository = assigningRepository;
        }


        [HttpGet("requests")]
        public IActionResult GetAllRequests()
        {
            var allRequests = _context.AssetMappings.Where(r => r.Status == AssetStatus.Pending).ToList();
            if (allRequests != null) return Ok(allRequests);
            return NotFound("No pending requests.");
        }

        [HttpPost("assign/{id}")]
        public IActionResult AssignAsset(int id, AssetType assetType)
        {
            var pendingRequests = _context.AssetMappings.Where(s => s.Status == AssetStatus.Pending).ToList();
            var asset = pendingRequests.SingleOrDefault(c => c.Id == id);
            if (asset == null) return NotFound("Request Not found");

            var assign = _assigningRepository.AssignAsset(id, assetType);
            return assign ? Ok("Asset assigned.") : BadRequest("Assignment failed.");
        }

        [HttpPost("unassign/{id}")]
        public IActionResult UnAssignAsset(int id, AssetType assetType)
        {
            var approvedRequests = _context.AssetMappings.Where(s => s.Status == AssetStatus.Approved).ToList();
            var asset = approvedRequests.SingleOrDefault(c => c.Id == id);
            if (asset == null) return NotFound("Request Not found");

            var result = _assigningRepository.UnassignAsset(id, assetType);
            return result ? Ok("Asset unassigned.") : NotFound("Mapping not found.");

        }

    }
}
