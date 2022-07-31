using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IPersonalInformationRepository
    {
        Task AddAsync(PersonalInformationDto user);
        Task UpdateAsync(PersonalInformationDto user);
        Task<List<PersonalInformationDto>> GetPaginationAsync(int page, int pageSize);
        Task<int> CountAsync();
        PersonalInformationDto GetUser(Guid id);
    }
}
