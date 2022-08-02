using AutoMapper;
using masa_backend.Models;

namespace masa_backend.ModelViews
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<PersonalInformation, PersonalInformationDto>().ReverseMap();
            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<WalletHistory, WalletHistoryDto>().ReverseMap();
        }
    }
}
