namespace masa_backend.Models
{
    public class User:BaseEntity
    {
        public int Type { get; set; } = 0;
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Key { get; set; }
        public string? Pass { get; set; }
        public Guid? PersonId { get; set; }
        public string? Files { get; set; }
        public PersonalInformation? PersonalInformation { get; set; }
    }
}
