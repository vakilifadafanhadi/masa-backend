namespace masa_backend.ModelViews
{
    public class PersonalInformationDto:BaseEntityDto
    {
        private string nationalCode = default!;
        private string lastName = default!;
        public string NationalCode { get => nationalCode; set => nationalCode = value; }
        public string? FatherFirstName { get; set; }
        public string? FatherLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get => lastName; set => nationalCode = value; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public CityDto? City { get; set; }
        public ProvinceDto? Province { get; set; }
        public CountryDto? Country { get; set; }
        public string Mobile { get; set; } = default!;
        public PersonalInformationDto()
        {
            City = new CityDto();
            Country = new CountryDto();
            Province = new ProvinceDto();
        }
    }
}
