using AssetAPI.Entity;
using AssetAPI.Enums;
using AssetAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.Web.Helpers;


namespace AssetAPI.Controllers
{
    [Route("[controller]/{assetType}")]
    [ApiController]
    [Authorize(Roles = "Customer,Admin")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository<Book> _bookRepository;
        private readonly IAssetRepository<SoftwareLicense> _softwareRepository;
        private readonly IAssetRepository<Hardware> _hardwareRepository;
        private readonly ILogger<LoginController> _logger;
        public AssetController(
            IAssetRepository<Book> bookRepository,
            IAssetRepository<SoftwareLicense> softwareRepository,
            IAssetRepository<Hardware> hardwareRepository,
            ILogger<LoginController> logger)
        {
            _bookRepository = bookRepository;
            _softwareRepository = softwareRepository;
            _hardwareRepository = hardwareRepository;
            _logger = logger;
        }

        private object? GetService(AssetType assetType)
        {
            return assetType switch
            {
                AssetType.Book => _bookRepository,
                AssetType.Software => _softwareRepository,
                AssetType.Hardware => _hardwareRepository,
                _ => null
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll(AssetType assetType)
        {
            var service = GetService(assetType);
            if (service == null) return BadRequest("Invalid asset type.");

            _logger.LogInformation("Request fetched successfully");
            return Ok(((dynamic)service).GetAll());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Search(AssetType assetType, int id)
        {
            var service = GetService(assetType);
            if (service == null) return BadRequest("Invalid asset type.");

            var result = ((dynamic)service).Search(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AssetType assetType, [FromBody] object asset)
        {
            //Console.WriteLine(asset);
            var service = GetService(assetType);
            if (service == null) return BadRequest("Invalid asset type.");

            dynamic? typedAsset = ConvertAsset(assetType, asset);
            if (typedAsset == null) return BadRequest("Invalid asset model.");

            var result = ((dynamic)service).Add(typedAsset);
            return CreatedAtAction(nameof(Search), new { assetType, id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(AssetType assetType, int id, [FromBody] object asset)
        {
            var service = GetService(assetType);
            if (service == null) return BadRequest("Invalid asset type.");

            dynamic? typedAsset = ConvertAsset(assetType, asset);
            if (typedAsset == null) return BadRequest("Invalid asset model.");

            var result = ((dynamic)service).Update(id, typedAsset);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(AssetType assetType, int id)
        {
            var service = GetService(assetType);
            if (service == null) return BadRequest("Invalid asset type.");

            bool deleted = ((dynamic)service).Delete(id);
            if (!deleted) return NotFound();
            return Ok("Asset Deleted");
        }

        private dynamic? ConvertAsset(AssetType assetType, object asset)
        {
            try
            {

                switch (assetType)
                {
                    case AssetType.Book:
                        var bookJson = JsonSerializer.Serialize(asset); // object to JSON
                        var bookTyped = JsonSerializer.Deserialize<Book>(bookJson); // JSON to Book
                        return bookTyped;
                    case AssetType.Software:
                        var softwareJson = JsonSerializer.Serialize(asset);
                        var softwareTyped = JsonSerializer.Deserialize<SoftwareLicense>(softwareJson);
                        return softwareTyped;
                    case AssetType.Hardware:
                        var hardwareJson = JsonSerializer.Serialize(asset);
                        var hardwareTyped = JsonSerializer.Deserialize<Hardware>(hardwareJson);
                        return hardwareTyped;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
