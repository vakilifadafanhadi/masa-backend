namespace masa_backend.Repositories
{
    public interface IRepositoryWrapper:IDisposable
    {
        bool IsDisposed { get; }
        IUserRepository UserRepository { get; }
        IPersonalInformationRepository PersonalInformationRepository { get; }
        IWalletRepository WalletRepository { get; }
        IWalletHistoryRepository WalletHistoryRepository { get; }
        ICountryRepository CountryRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
