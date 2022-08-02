using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;
using Microsoft.EntityFrameworkCore;

namespace masa_backend.Repositories
{
    public class PersonalInformationRepository:RepositoryBase<PersonalInformation>,IPersonalInformationRepository
    {
        public PersonalInformationRepository(MasaDbContext repositoryContext, IMapper mapper) :base(repositoryContext,mapper)
        {
        }
        public async Task<PersonalInformationDto> AddAsync(PersonalInformationDto personalInformation)
        {
            await AddAsync(_mapper.Map<PersonalInformation>(personalInformation));
            return _mapper.Map<PersonalInformationDto>(personalInformation);
        }
        public async Task UpdateAsync(PersonalInformationDto personalInformation)
        {
            await UpdateAsync(_mapper.Map<PersonalInformation>(personalInformation));
        }
        public async Task<List<PersonalInformationDto>> GetPaginationAsync(int page, int pageSize)
        {
            return await GetByQuery()
                .Select(current => _mapper.Map<PersonalInformationDto>(current))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await GetByQuery()
                .CountAsync();
        }
        public PersonalInformationDto Get(Guid id)
        {
            var result = GetByQuery()
                .Where(current => current.Id == id);
            return _mapper.Map<PersonalInformationDto>(result);
        }
        public PersonalInformationDto GetByUserId(Guid userId)
        {
            var result = GetByQuery()
                .Where(current=>current.UserId == userId);
            return _mapper.Map<PersonalInformationDto>(result);
        }
    }
}
