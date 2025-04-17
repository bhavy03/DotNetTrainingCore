
namespace AssetAPI.Entity
{
    public class SoftwareLicense : Asset
    {
        public string? LicenseKey { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
