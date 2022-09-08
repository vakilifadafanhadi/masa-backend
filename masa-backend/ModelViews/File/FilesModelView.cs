namespace masa_backend.ModelViews
{
    public class FileDetails
    {
        public string Name { get; set; } = default!;
        public string Path { get; set; } = default!;
    }

    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
    }
}