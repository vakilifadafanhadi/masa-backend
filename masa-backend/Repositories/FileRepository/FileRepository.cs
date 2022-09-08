using AutoMapper;
using masa_backend.ModelViews;
using Microsoft.EntityFrameworkCore;
using File = masa_backend.Models.File;

namespace masa_backend.Repositories
{
    public class FileRepository:RepositoryBase<File>,IFileRepository
    {
        public FileRepository(MasaDbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper) { }
        public async Task<FileDto> AddAsync(FileDto file)
        {
            await AddAsync(_mapper.Map<File>(file));
            return file;
        }
        public async Task<FileDto> UpdateAsync(FileDto file)
        {
            await UpdateAsync(_mapper.Map<File>(file));
            return file;
        }
        public async Task DeleteAsync(Guid fileId)
        {
            var file = GetByQuery().
                Where(current=>current.Id==fileId)
                .FirstOrDefault();
            if (file != null)
            {
                file.RemoveAt = DateTime.Now;
                await UpdateAsync(_mapper.Map<File>(file));
            }
        }
        public FileDto Get(Guid fileId) 
        {
            return _mapper.Map<FileDto>(
                GetByQuery().
                    Where(current => current.Id == fileId)
                    .FirstOrDefault());
        }
        public async Task<List<FileDto>> ListAsync(Guid userId)
        {
            return await GetByQuery()
                .Where(current => current.UserId == userId)
                .Where(current => current.RemoveAt == null)
                .Select(current=>_mapper.Map<FileDto>(current))
                .ToListAsync();
        }
    }
}
