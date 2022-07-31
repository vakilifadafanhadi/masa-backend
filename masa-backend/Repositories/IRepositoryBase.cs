namespace masa_backend.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        IQueryable<T> GetByQuery();
    }
}
