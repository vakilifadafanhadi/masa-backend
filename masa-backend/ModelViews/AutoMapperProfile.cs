using AutoMapper;
using masa_backend.Models;

namespace masa_backend.ModelViews
{
    public class AutoMapperProfile: Profile
    {
        public override string ProfileName
        {
            get
            {
                return "AutoMapper";
            }
        }
        public void ConfigureMappings()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<PersonalInformation, PersonalInformationDto>().ReverseMap();
            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            //CreateMap<List<User>, List<UserDto>>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<WalletHistory, WalletHistoryDto>().ReverseMap();
        }
        public AutoMapperProfile()
        {
            ConfigureMappings();
        }
    }
}
