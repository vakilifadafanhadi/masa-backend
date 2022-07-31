namespace masa_backend.Models
{
    public class Country:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string PersianName { get; set; } = default!;
        public int Code { get; set; }
        public string Continent { get; set; } = default!;
    }
}
