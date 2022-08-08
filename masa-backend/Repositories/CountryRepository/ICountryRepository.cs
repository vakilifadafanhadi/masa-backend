using masa_backend.ModelViews;

namespace masa_backend.Repositories
{
    public interface ICountryRepository
    {
        Task<CountryDto> AddAsync(CountryDto country);
    }
}
