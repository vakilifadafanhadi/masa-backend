namespace masa_backend.ModelViews
{
    public class FileDto:BaseEntityDto
    {
        public string? Name { get; set; }
        public Guid? UserId { get; set; }
    }
}
