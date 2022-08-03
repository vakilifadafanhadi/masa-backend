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
        public async Task<WalletDto> AddAsync(WalletDto wallet)
        {
            await AddAsync(_mapper.Map<Wallet>(wallet));
            return wallet;
        }
        public async Task UpdateAsync(WalletDto wallet)
        {
            wallet.UpdateAt = DateTime.Now;
            await UpdateAsync(_mapper.Map<Wallet>(wallet));
        }
        public WalletDto Get(Guid id)
        {
            return _mapper.Map<WalletDto>(
                GetByQuery()
                .Where(current => current.Id == id).FirstOrDefault());
        }
        public WalletDto GetByPersonId(Guid personId)
        {
            return _mapper.Map<WalletDto>(
                GetByQuery()
                .Where(current=>current.PersonId==personId).FirstOrDefault());
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
