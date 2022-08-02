using System.ComponentModel.DataAnnotations;

namespace masa_backend.Models
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public int CountryCode { get; set; }
        public string? Continent { get; set; }
        public List<Province>? Provinces { get; set; }
        public List<City>? Citys { get; set; }
        public List<PersonalInformation>? PersonalInformation { get; set; }
    }
}
