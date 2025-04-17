using AssetAPI.Entity;

namespace AssetAPI.Repositories
{
    public class SoftwareRepository : IAssetRepository<SoftwareLicense>
    {

        private readonly AssetContext _context;
        public SoftwareRepository(AssetContext context)
        {
            _context = context;
        }

        public IEnumerable<SoftwareLicense> GetAll()
        {
            return _context.Softwares.ToList();
        }

        public SoftwareLicense? Search(int id)
        {
            var software = _context.Softwares.FirstOrDefault(c => c.Id == id);
            if (software != null) return software;
            else return null;
        }

        public SoftwareLicense Add(SoftwareLicense asset)
        {
            _context.Softwares.Add(asset);
            _context.SaveChanges();
            return asset;

        }

        public bool Delete(int id)
        {
            var software = _context.Softwares.Find(id);
            if (software == null) return false;

            _context.Softwares.Remove(software);
            _context.SaveChanges();
            return true;
        }

        public SoftwareLicense? Update(int id, SoftwareLicense asset)
        {
            var existing = _context.Softwares.Find(id);
            if (existing == null) return null;

            existing.Name = asset.Name;
            existing.ExpiryDate = asset.ExpiryDate;
            existing.LicenseKey = asset.LicenseKey;
            existing.QuantityAvailable = asset.QuantityAvailable;

            _context.SaveChanges();
            return existing;
        }
    }
}
