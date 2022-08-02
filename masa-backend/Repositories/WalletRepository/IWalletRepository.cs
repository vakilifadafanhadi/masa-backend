using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IWalletRepository
    {
        Task AddAsync(WalletDto wallet);
        string GetBalance(Guid personId);
    }
}
