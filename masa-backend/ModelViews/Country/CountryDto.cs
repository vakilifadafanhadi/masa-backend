﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace masa_backend.ModelViews
{
    public class CountryDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = default!;
        public int? CountryCode { get; set; }
        [StringLength(3)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Continent { get; set; }
        public List<ProvinceDto>? Provinces { get; set; }
        public List<CityDto>? Cities { get; set; }
        public List<PersonalInformationDto>? PersonalInformation { get; set; }
        //public CountryDto()
        //{
        //    Cities = new List<CityDto>();
        //    PersonalInformation = new List<PersonalInformationDto>();
        //    Provinces = new List<ProvinceDto>();
        //}
    }
}
