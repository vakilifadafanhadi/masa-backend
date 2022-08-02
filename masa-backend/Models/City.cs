namespace masa_backend.Models
{
    public class City : BaseEntity
    {
        public string? Name { get; set; }
        public int? CityCode { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }
        public Country? Country { get; set; }
        public Province? Province { get; set; }
        public List<PersonalInformation>? PersonalInformation { get; set; }
        public City()
        {
            PersonalInformation = new List<PersonalInformation>();
            Country = new Country();
            Province = new Province();
        }
    }
}