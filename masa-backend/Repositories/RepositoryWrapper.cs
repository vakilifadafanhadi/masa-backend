using AutoMapper;

namespace masa_backend.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        protected MasaDbContext _masaDbContext;
        private readonly IMapper _mapper;
        public bool IsDisposed { get; protected set; }
        private IUserRepository? _userRepository;
        private IPersonalInformationRepository? _personalInformationRepository;
        private IWalletRepository? _walletRepository;
        private IWalletHistoryRepository? _walletHistoryRepository;
        private ICountryRepository? _countryRepository;
        private IFileRepository? _fileRepository;
        public RepositoryWrapper(MasaDbContext masaDbContext, IMapper mapper)
        {
            _masaDbContext = masaDbContext;
            _mapper = mapper;
        }
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? new UserRepository(_masaDbContext, _mapper);
            }
        }
        public async Task SaveAsync()
        {
            await _masaDbContext.SaveChangesAsync();
        }
        public void Save()
        {
            _masaDbContext.SaveChanges();
        }
        public IWalletRepository WalletRepository
        {
            get
            {
                return _walletRepository ?? new WalletRepository(_masaDbContext, _mapper);
            }
        }
        public IWalletHistoryRepository WalletHistoryRepository
        {
            get
            {
                return _walletHistoryRepository ?? new WalletHistoryRepository(_masaDbContext, _mapper);
            }
        }
        public IPersonalInformationRepository PersonalInformationRepository
        {
            get
            {
                return _personalInformationRepository ?? new PersonalInformationRepository(_masaDbContext, _mapper);
            }
        }
        public ICountryRepository CountryRepository
        {
            get
            {
                return _countryRepository ?? new CountryRepository(_masaDbContext, _mapper);
            }
        }
        public IFileRepository FileRepository
        {
            get
            {
                return _fileRepository ?? new FileRepository(_masaDbContext, _mapper);
            }
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
                if (_masaDbContext != null)
                {
                    _masaDbContext!.Dispose();
                    _masaDbContext = null!;
                }
            }
        }
    }
}
