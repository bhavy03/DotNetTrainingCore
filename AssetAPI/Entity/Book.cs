

namespace AssetAPI.Entity
{
    public class Book : Asset
    {
        public required string Author { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
