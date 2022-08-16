using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;
using Microsoft.EntityFrameworkCore;

namespace masa_backend.Repositories
{
    public class UserRepository:RepositoryBase<User>,IUserRepository
    {
        public UserRepository(MasaDbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
        }
        public async Task AddAsync(UserDto user)
        {
            await AddAsync(_mapper.Map<User>(user));
        }
        public async Task UpdateAsync(UserDto user)
        {
            await UpdateAsync(_mapper.Map<User>(user));
        }
        public async Task<List<UserDto>> GetPaginationAsync(int page, int pageSize)
        {
            return await GetByQuery()
                .Select(current => _mapper.Map<UserDto>(current))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await GetByQuery()
                .CountAsync();
        }
        public UserDto GetUser(Guid id)
        {
            var result = GetByQuery()
                .Where(current => current.Id == id).FirstOrDefault();
            return _mapper.Map<UserDto>(result);
        }
        public UserDto Login(LoginModelView model)
        {
            try
            {
                long.Parse(model.UserName);
                return _mapper.Map<UserDto>(GetByQuery()
                    .Where(current => current.PersonalInformation.NationalCode == model.UserName ||
                    current.PersonalInformation.Mobile == model.UserName)
                    .Where(current => current.Pass == model.Pass)
                    .FirstOrDefault());
            }
            catch (FormatException)
            {
                return _mapper.Map<UserDto>(GetByQuery()
                    .Where(current => current.PersonalInformation.Email == model.UserName)
                    .Where(current => current.Pass == model.Pass).FirstOrDefault());
            }
        }
    }
}
