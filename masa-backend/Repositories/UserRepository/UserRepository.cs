using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<List<UserDto>> GetPaginationAsync(int page, int pageSize, int? userType)
        {
            var query = GetByQuery()
                .Select(current=>_mapper.Map<UserDto>(current));
            if(userType!=null)
                query = query.Where(x => x.Type==userType);
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(current=>current.CreateAt)
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
        public UserDto GetByPersonId(Guid personId)
        {
            return _mapper.Map<UserDto>(
                GetByQuery()
                .Where(current => current.PersonId == personId)
                .FirstOrDefault()
                );
        }
        public UserDto Login(LoginModelView model)
        {
            return _mapper.Map<UserDto>(
                GetByQuery()
                .Where(current => current.PersonalInformation.PersonalIdentity == model.PersonalIdentity)
                .Where(current => current.Pass == model.Pass)
                .FirstOrDefault());
        }
        public UserDto GetByToken(string token)
        {
            return _mapper.Map<UserDto>(
                GetByQuery()
                .Where(current => current.Token == token)
                .FirstOrDefault());
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            var userList = await GetByQuery()
                .OrderBy(current=>current.CreateAt)
                .Select(current => _mapper.Map<UserDto>(current))
                .ToListAsync();
            return _mapper.Map<List<UserDto>>(userList);
        }
        public UserDto GetByPersonalityId(string personalityId) 
        {
            return _mapper.Map<UserDto>(GetByQuery()
                .Where(current => current.Token == personalityId)
                .FirstOrDefault());
        }
        public int Count()
        {
            return GetByQuery()
                .Count();
        }
    }
}
