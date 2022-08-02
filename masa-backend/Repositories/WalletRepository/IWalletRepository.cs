using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletDto> AddAsync(WalletDto wallet);
        Task UpdateAsync(WalletDto wallet);
        string GetBalance(Guid personId);
        WalletDto Get(Guid id);
        WalletDto GetByPersonId(Guid id);
    }
}
