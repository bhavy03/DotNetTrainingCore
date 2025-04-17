using AssetAPI.Enums;

namespace AssetAPI.Repositories
{
    public class AssetAssignRequest
    {
        public AssetType assetType { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
    }
}
