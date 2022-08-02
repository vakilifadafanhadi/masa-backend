using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface IWalletHistoryRepository
    {
        Task<WalletHistoryDto> AddAsync(WalletHistoryDto walletHistory);
    }
}
