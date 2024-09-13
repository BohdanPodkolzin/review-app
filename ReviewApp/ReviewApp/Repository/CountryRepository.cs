using ReviewApp.Context;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Repository
{
    public class CountryRepository(PokemonContext context) : ICountryRepository
    {
        private readonly PokemonContext _context = context;
        public ICollection<Country> GetCountries()
            => _context.Countries.OrderBy(x => x.Id).ToList();

        public Country? GetCountry(int id)
            => _context.Countries.SingleOrDefault(x => x != null && x.Id == id);

        public ICollection<Owner?> GetOwnersByCountry(int countryId)
            => _context.Owners.Where(owner => owner.CountryId == countryId).ToList();

        public Country? GetCountryByOwner(int ownerId)
            => _context.Owners
                .Where(o => o.Id == ownerId)
                .Select(c => c.Country)
                .SingleOrDefault();

        public bool IsCountryExists(int id)
            => _context.Countries.Any(c => c.Id == id);

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public bool Save()
            => _context.SaveChanges() > 0;
    }
}
