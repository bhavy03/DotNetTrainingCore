using Microsoft.EntityFrameworkCore;

namespace AssetAPI.Repositories
{
    public class ChangeQuantity
    {
        public bool ReduceQuantity<T>(DbSet<T> dbset, int assetId, int Quantity) where T : class
        {
            dynamic asset = dbset.Find(assetId);
            if (asset != null && asset?.QuantityAvailable >= Quantity)
            {
                asset.QuantityAvailable -= Quantity;
                return true;
            }
            return false;

        }

        public void IncreaseQuantity<T>(DbSet<T> dbset, int assetId, int Quantity) where T : class
        {
            dynamic asset = dbset.Find(assetId);
            if (asset != null)
            {
                asset.QuantityAvailable += Quantity;
            }
        }

        public bool CheckQuantity<T>(DbSet<T> dbset, int assetId, int Quantity) where T : class
        {
            dynamic asset = dbset.Find(assetId);
            if (asset != null && asset?.QuantityAvailable >= Quantity)
            {
                return true;
            }
            return false;
        }
    }
}
