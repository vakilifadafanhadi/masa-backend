using System.ComponentModel.DataAnnotations;

namespace masa_backend.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string PersianName { get; set; } = default!;
        [Required]
        public int CountryCode { get; set; }
        public string? Continent { get; set; }
        public List<Province> Provinces { get; set; }
        public Country()
        {
            Provinces = new List<Province>();
        }
    }
}
