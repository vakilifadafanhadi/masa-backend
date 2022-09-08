using AutoMapper;
using masa_backend.Models;
using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public class CountryRepository:RepositoryBase<Country>,ICountryRepository
    {
        public CountryRepository(MasaDbContext repositoryContext, IMapper mapper) : base(repositoryContext, mapper) { }
        public async Task<CountryDto> AddAsync(CountryDto country)
        {
            var oldCountry = _mapper.Map<CountryDto>(GetByQuery()
                .Where(current => current.CountryCode == country.CountryCode)
                .FirstOrDefault());
            if (oldCountry == null)
            {
                oldCountry = country;
                await AddAsync(_mapper.Map<Country>(country));
            }
            return oldCountry;
        }
        public CountryDto Get(Guid id)
        {
            return _mapper.Map<CountryDto>(GetByQuery()
                .Where(current => current.Id == id).FirstOrDefault());
        }
    }
}
