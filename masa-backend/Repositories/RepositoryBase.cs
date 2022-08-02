using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace masa_backend.Repositories
{
    public class RepositoryBase<T>:IRepositoryBase<T> where T : class
    {
        protected MasaDbContext RepositoryContext;
        public readonly IMapper _mapper;
        public RepositoryBase(MasaDbContext repositoryContext, IMapper mapper)
        {
            RepositoryContext = repositoryContext;
            _mapper = mapper;
        }
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(paramName: nameof(entity));
            await RepositoryContext.Set<T>().AddAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                RepositoryContext.Set<T>().Update(entity);
            });
        }
        public IQueryable<T> GetByQuery()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }
        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
        public void Save()
        {
            RepositoryContext.SaveChanges();
        }
    }
}
