using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IFileRepository
    {
        Task<FileDto> AddAsync(FileDto file);
        Task<FileDto> UpdateAsync(FileDto file);
        Task DeleteAsync(Guid fileId);
        FileDto Get(Guid fileId);
        Task<List<FileDto>> ListAsync(Guid userId);
    }
}
