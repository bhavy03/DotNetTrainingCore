using AssetAPI.Entity;


namespace AssetAPI.Repositories
{
    public class HardwareRepository : IAssetRepository<Hardware>
    {
        private readonly AssetContext _context;
        public HardwareRepository(AssetContext context)
        {
            _context = context;
        }
        public IEnumerable<Hardware> GetAll()
        {
            return _context.Hardwares.ToList();
        }

        public Hardware? Search(int id)
        {
            var hardware = _context.Hardwares.FirstOrDefault(c => c.Id == id);
            if (hardware != null) return hardware;
            else return null;
        }

        public Hardware Add(Hardware asset)
        {
            _context.Hardwares.Add(asset);
            _context.SaveChanges();
            return asset;
        }

        public bool Delete(int id)
        {
            var hardware = _context.Hardwares.Find(id);
            if (hardware == null) return false;

            _context.Hardwares.Remove(hardware);
            _context.SaveChanges();
            return true;
        }

        public Hardware? Update(int id, Hardware asset)
        {
            var existing = _context.Hardwares.Find(id);
            if (existing == null) return null;

            existing.Name = asset.Name;
            existing.Manufacturer = asset.Manufacturer;
            existing.QuantityAvailable = asset.QuantityAvailable;

            _context.SaveChanges();
            return existing;
        }
    }
}
