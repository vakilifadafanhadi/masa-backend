using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;
using Microsoft.EntityFrameworkCore;

namespace masa_backend.Repositories
{
    public class WalletHistoryRepository:RepositoryBase<WalletHistory>, IWalletHistoryRepository
    {
        public WalletHistoryRepository(MasaDbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper)
        {
        }
        public async Task<WalletHistoryDto> AddAsync(WalletHistoryDto walletHistory)
        {
            await AddAsync(_mapper.Map<WalletHistory>(walletHistory));
            return walletHistory;
        }
        public async Task<List<WalletHistoryDto>> ListAsync(Guid? walletId)
        {
            return _mapper.Map<List<WalletHistoryDto>>(await GetByQuery()
                .Where(current => current.WalletId == walletId)
                .ToListAsync());
        }
    }
}
