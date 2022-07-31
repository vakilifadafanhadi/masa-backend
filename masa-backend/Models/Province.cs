namespace masa_backend.Models
{
    public class Province:BaseEntity
    {
        public string Name { get; set; } = default!;
        public int Code { get; set; }
        public Country Country { get; set; }
        public Province()
        {
            Country = new Country();
        }
    }
}
