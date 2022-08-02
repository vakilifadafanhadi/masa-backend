using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class ProvinceDto:BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = default!;
        public int? ProvinceCode { get; set; }
        public Guid? CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public List<CityDto>? Citys { get; set; }
        public List<PersonalInformationDto>? PersonalInformation { get; set; }
        //public ProvinceDto()
        //{
        //    Citys = new List<CityDto>();
        //    Country = new CountryDto();
        //    PersonalInformation = new List<PersonalInformationDto>();
        //}
    }
}
