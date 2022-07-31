namespace masa_backend.ModelViews
{
    public class UserDto:BaseEntityDto
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Key { get; set; }
        public string? Pass { get; set; }
        public string NationalCode { get; set; } = default!;
    }
}
