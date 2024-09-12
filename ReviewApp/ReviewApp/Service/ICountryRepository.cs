using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewApp.Models;

namespace ReviewApp.Service
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country? GetCountry(int id);
        ICollection<Owner?> GetOwnersByCountry(int countryId);
        Country? GetCountryByOwner(int ownerId);
        bool IsCountryExists(int id);

        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        protected bool Save();
    }
}
