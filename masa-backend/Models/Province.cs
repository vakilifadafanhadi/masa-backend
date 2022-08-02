namespace masa_backend.Models
{
    public class Province : BaseEntity
    {
        public string? Name { get; set; }
        public int? ProvinceCode { get; set; }
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
        public List<City>? Citys { get; set; }
        public List<PersonalInformation>? PersonalInformation { get; set; }
    }
}