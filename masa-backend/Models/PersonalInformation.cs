namespace masa_backend.Models
{
    public class PersonalInformation : BaseEntity
    {
        public string? NationalCode { get; set; }
        public string? FatherFirstName { get; set; }
        public string? FatherLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public Guid? WalletId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? CountryId { get; set; }
        public City? City { get; set; }
        public Country? Country { get; set; }
        public Province? Province { get; set; }
        public Wallet? Wallet { get; set; }
        public User? User { get; set; }
    }
}
