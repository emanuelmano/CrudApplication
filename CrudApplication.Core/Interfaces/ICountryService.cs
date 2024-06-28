using CrudApplication.Models;
using CrudApplication.Models.Entities;


namespace CrudApplication.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetAllCountriesAsync(int skip, int take);
        Task<CountryModel> GetCountryByIdAsync(int id);
        Task<int> CreateCountryAsync(CountryModel countryModel);
        Task<int> UpdateCountryAsync(CountryModel countryModel);
        Task<int> DeleteCountryAsync(int id);
    }
}
