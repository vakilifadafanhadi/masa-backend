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
        UserDto Login(LoginModelView user);
        UserDto GetByToken(string token);
        UserDto GetByPersonId(Guid personId);
        UserDto GetByPersonalityId(string personalityId);
        Task<List<UserDto>> GetAllAsync();
        int Count();
    }
}
