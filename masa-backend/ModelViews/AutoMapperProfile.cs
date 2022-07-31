using AutoMapper;
using masa_backend.Models;

namespace masa_backend.ModelViews
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<PersonalInformation, PersonalInformationDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}
