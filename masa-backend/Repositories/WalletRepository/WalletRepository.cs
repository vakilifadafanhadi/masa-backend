using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
    {
        public WalletRepository(MasaDbContext repositoryContext, IMapper mapper):base(repositoryContext,mapper)
        {
        }
        public async Task AddAsync(WalletDto wallet)
        {
            await AddAsync(_mapper.Map<Wallet>(wallet));
        }
        public string GetBalance(Guid personId)
        {
            var result = GetByQuery()
                .Where(current => current.PersonId == personId)
                .Select(current => current.Amount)
                .FirstOrDefault();
            return result ?? "0";
        }
    }
}
