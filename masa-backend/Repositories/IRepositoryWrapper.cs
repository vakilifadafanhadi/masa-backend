namespace masa_backend.Repositories
{
    public interface IRepositoryWrapper:IDisposable
    {
        bool IsDisposed { get; }
        IUserRepository UserRepository { get; }
        IPersonalInformationRepository PersonalInformationRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
