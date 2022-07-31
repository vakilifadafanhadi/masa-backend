namespace masa_backend.ModelViews
{
    public class PersonalInformationDto:BaseEntityDto
    {
        public string NationalCode { get; set; } = default!;
        public string? FatherFirstName { get; set; }
        public string? FatherLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = default!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string Mobile { get; set; } = default!;
    }
}
