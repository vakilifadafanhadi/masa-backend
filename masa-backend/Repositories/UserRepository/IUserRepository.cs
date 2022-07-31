using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserDto user);
        Task UpdateAsync(UserDto user);
        Task<List<UserDto>> GetPaginationAsync(int page, int pageSize);
        Task<int> CountAsync();
        UserDto GetUser(Guid id);
    }
}
