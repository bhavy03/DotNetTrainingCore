using AssetAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetAPI.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Number must be exactly 10 digits.")]
        public string? Number { get; set; }
        public string? Address { get; set; }
        public Role? Role { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<AssetMapping>? AssetMappings { get; set; }
    }
}
