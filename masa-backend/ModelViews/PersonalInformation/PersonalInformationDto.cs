using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class PersonalInformationDto:BaseEntityDto
    {
        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; } = default!;
        public string? FatherFirstName { get; set; }
        public string? FatherLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        [Required]
        public string LastName { get; set; } = default!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        [Required]
        [MinLength(10), MaxLength(13)]
        public string Mobile { get; set; } = default!;
        public Guid? WalletId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? CountryId { get; set; }
        public CityDto? City { get; set; }
        public CountryDto? Country { get; set; }
        public ProvinceDto? Province { get; set; }
        public WalletDto? Wallet { get; set; }
        public UserDto? User { get; set; }
        //public PersonalInformationDto()
        //{
        //    City = new CityDto();
        //    Country = new CountryDto();
        //    Province = new ProvinceDto();
        //    Wallet = new WalletDto();
        //    User = new UserDto();
        //}
    }
}
