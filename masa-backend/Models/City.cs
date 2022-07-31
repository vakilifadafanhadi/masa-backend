namespace masa_backend.Models
{
    public class City:BaseEntity
    {
        public string Name { get; set; } = default!;
        public int CountryCode { get; set; } = default!;
        public int ProvinceCode { get; set; }
        public Province Province { get; set; }
        public City()
        {
            Province = new Province();
        }
    }
}
