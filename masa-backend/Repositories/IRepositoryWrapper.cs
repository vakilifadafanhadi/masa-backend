namespace masa_backend.Repositories
{
    public interface IRepositoryWrapper
    {
        bool IsDisposed { get; }
        IUserRepository UserRepository { get; }
        IPersonalInformationRepository PersonalInformationRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
