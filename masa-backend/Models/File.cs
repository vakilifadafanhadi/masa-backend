namespace masa_backend.Models
{
    public class File:BaseEntity
    {
        public string? Name { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
