namespace masa_backend.ModelViews
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? RemoveAt { get; set; }
        public BaseEntityDto()
        {
            CreateAt = DateTime.Now;
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
        }
    }
}
