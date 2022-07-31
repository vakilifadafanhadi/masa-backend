using AutoMapper;
using masa_backend.Repositories;

namespace masa_backend.Repositories
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        protected MasaDbContext _masaDbContext;
        private readonly IMapper _mapper;
        public bool IsDisposed { get; protected set; }
        private readonly IUserRepository? _userRepository;
        private readonly IPersonalInformationRepository? _personalInformationRepository;
        public RepositoryWrapper(MasaDbContext masaDbContext, IMapper mapper)
        {
            _masaDbContext = masaDbContext;
            _mapper = mapper;
        }
        public IUserRepository UserRepository => 
            _userRepository ?? 
            new UserRepository(_masaDbContext, _mapper);
        public IPersonalInformationRepository PersonalInformationRepository =>
            _personalInformationRepository ??
            new PersonalInformationRepository(_masaDbContext, _mapper);
        public async Task SaveAsync()
        {
            await _masaDbContext.SaveChangesAsync();
        }
        public void Save()
        {
            _masaDbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;
            if (disposing)
            {
                _masaDbContext!.Dispose();
                _masaDbContext = null;
            }
        }
    }
}
