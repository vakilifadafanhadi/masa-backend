namespace masa_backend.Models
{
    public class City:BaseEntity
    {
        public string Name { get; set; } = default!;
        public Province Province { get; set; }
        public Country Country { get; set; }
        public City()
        {
            Province = new Province();
            Country = new Country();
        }
    }
}
