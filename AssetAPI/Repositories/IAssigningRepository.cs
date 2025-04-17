using AssetAPI.Enums;

namespace AssetAPI.Repositories
{
    public interface IAssigningRepository
    {
        bool RequestAsset(int customerId, AssetType assetType, int assetId, int quantity);
        bool AssignAsset(int assetId, AssetType assetType);
        bool UnassignAsset(int assetId, AssetType assetType);
    }
}
