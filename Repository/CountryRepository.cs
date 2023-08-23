using AutoMapper;
using PokemonWebApp.Data;
using PokemonWebApp.Interfaces;
using PokemonWebApp.Models;
using System.Linq;

namespace PokemonWebApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private DataContext _context;
        private IMapper _mapper;
       
        public CountryRepository(DataContext context, IMapper mapper)
        {
                _context = context;
                _mapper = mapper;
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(p => p.Id).ToList();  
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(p => p.Id == ownerId).Select(p => p.Country).FirstOrDefault();   
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _context.Owners.Where(p => p.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
