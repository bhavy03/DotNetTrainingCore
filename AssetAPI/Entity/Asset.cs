namespace AssetAPI.Entity
{
    public class Asset
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int QuantityAvailable { get; set; }
        public ICollection<AssetMapping>? AssetMappings { get; set; }
    }
}
