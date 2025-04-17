using AssetAPI.Enums;

namespace AssetAPI.Entity
{
    public class AssetMapping
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int AssetId { get; set; }
        public Asset? Asset { get; set; }
        public int Quantity { get; set; }
        public AssetStatus? Status { get; set; }
        public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
    }
}
