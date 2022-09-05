namespace masa_backend.ModelViews
{
    public class UserPagination
    {
        public int Count { get; set; }
        public List<UserDto> Data { get; set; }
        public UserPagination()
        {
            Data = new List<UserDto>();
        }
    }
}
