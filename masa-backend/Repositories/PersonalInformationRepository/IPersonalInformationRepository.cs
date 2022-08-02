using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IPersonalInformationRepository
    {
        Task<PersonalInformationDto> AddAsync(PersonalInformationDto person);
        Task UpdateAsync(PersonalInformationDto person);
        Task<List<PersonalInformationDto>> GetPaginationAsync(int page, int pageSize);
        Task<int> CountAsync();
        PersonalInformationDto Get(Guid id);
        PersonalInformationDto GetByUserId(Guid userId);
    }
}
