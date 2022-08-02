using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class CityDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = default!;
        public int? CityCode { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }
        public CountryDto? Country { get; set; }
        public ProvinceDto? Province { get; set; }
        public List<PersonalInformationDto>? PersonalInformation { get; set; }
        //public CityDto()
        //{
        //    if (true)
        //    {

        //    }
        //    PersonalInformation = new List<PersonalInformationDto>();
        //    Country = new CountryDto();
        //    Province = new ProvinceDto();
        //}
    }
}
