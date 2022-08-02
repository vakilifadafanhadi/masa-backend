namespace masa_backend.ModelViews
{
    public class ResponceMV
    {
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public void Add(Exception ex)
        {
            if(!string.IsNullOrEmpty(ex?.Message))
            Errors.Add(ex.Message);
            if (!string.IsNullOrEmpty(ex?.InnerException?.Message))
                Add(ex.InnerException);
        }
        public ResponceMV()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
        }
    }
}
