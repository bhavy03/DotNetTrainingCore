using AssetAPI.Entity;
using AssetAPI.Enums;

namespace AssetAPI.Repositories
{
    public class AssigningRepository : IAssigningRepository
    {
        private readonly AssetContext _context;

        public AssigningRepository(AssetContext context)
        {
            _context = context;
        }

        public bool RequestAsset(int customerId, AssetType assetType, int assetId, int quantity)
        {
            bool isAvailable = false;

            var existingMapping = _context.AssetMappings
                .FirstOrDefault(m => m.CustomerId == customerId && m.AssetId == assetId);

            ChangeQuantity changeQuantity = new ChangeQuantity();
            switch (assetType)
            {
                case AssetType.Book:
                    isAvailable = changeQuantity.CheckQuantity(_context.Books, assetId, quantity);
                    //: changeQuantity.ReduceQuantity(_context.Books, assetId, quantity);
                    break;
                case AssetType.Hardware:
                    isAvailable = changeQuantity.CheckQuantity(_context.Hardwares, assetId, quantity);
                    //: changeQuantity.ReduceQuantity(_context.Hardwares, assetId, quantity);
                    break;
                case AssetType.Software:
                    isAvailable = changeQuantity.CheckQuantity(_context.Softwares, assetId, quantity);
                    //: changeQuantity.ReduceQuantity(_context.Softwares, assetId, quantity);
                    break;
                default:
                    isAvailable = false;
                    break;
            }

            if (!isAvailable) return false;

            if (existingMapping != null)
            {
                existingMapping.Quantity += quantity;
            }
            else
            {

                var newMapping = new AssetMapping
                {
                    CustomerId = customerId,
                    AssetId = assetId,
                    Quantity = quantity,
                    Status = AssetStatus.Pending
                };
                _context.AssetMappings.Add(newMapping);
            }

            _context.SaveChanges();
            return true;
        }
        public bool AssignAsset(int assetMappingId, AssetType assetType)
        {
            var request = _context.AssetMappings.Find(assetMappingId);
            if (request == null) return false;

            bool isAvailable = false;

            int quantity = request.Quantity;
            ChangeQuantity changeQuantity = new ChangeQuantity();
            switch (assetType)
            {
                case AssetType.Book:
                    isAvailable = changeQuantity.ReduceQuantity(_context.Books, request.AssetId, quantity);
                    break;
                case AssetType.Hardware:
                    isAvailable = changeQuantity.ReduceQuantity(_context.Hardwares, request.AssetId, quantity);
                    break;
                case AssetType.Software:
                    isAvailable = changeQuantity.ReduceQuantity(_context.Softwares, request.AssetId, quantity);
                    break;
                default:
                    isAvailable = false;
                    break;
            }

            if (!isAvailable) return false;

            request.Status = AssetStatus.Approved;
            _context.SaveChanges();
            return true;
        }
        public bool UnassignAsset(int assetMappingId, AssetType assetType)
        {
            var mapping = _context.AssetMappings.Find(assetMappingId);
            if (mapping == null) return false;

            int quantity = mapping.Quantity;

            ChangeQuantity changeQuantity = new ChangeQuantity();
            switch (assetType)
            {
                case AssetType.Book:
                    changeQuantity.IncreaseQuantity(_context.Books, mapping.AssetId, quantity);
                    break;
                case AssetType.Hardware:
                    changeQuantity.IncreaseQuantity(_context.Hardwares, mapping.AssetId, quantity);
                    break;
                case AssetType.Software:
                    changeQuantity.IncreaseQuantity(_context.Softwares, mapping.AssetId, quantity);
                    break;
                default:
                    break;
            }

            _context.AssetMappings.Remove(mapping);
            _context.SaveChanges();
            return true;
        }

    }
}
