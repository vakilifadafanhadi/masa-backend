using System.ComponentModel.DataAnnotations;

namespace masa_backend.Models
{
    public class Province:BaseEntity
    {
        public string Name { get; set; } = default!;
        public int ProvinceCode { get; set; }
        [Required]
        public int CountryCode { get; set; }
        public Country Country { get; set; }
        public List<City> Cities { get; set; }
        public Province()
        {
            Country = new Country();
            Cities = new List<City>();
        }
    }
}
