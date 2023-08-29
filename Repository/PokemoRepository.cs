using Microsoft.AspNetCore.Mvc;
using PokemonWebApp.Data;
using PokemonWebApp.Dto;
using PokemonWebApp.Interfaces;
using PokemonWebApp.Models;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace PokemonWebApp.Repository
{
    public class PokemoRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemoRepository(DataContext context)
        {
                _context = context; 
        }

        public bool CreatePokemon(int ownerId,int categoryId,Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
            _context.Add(pokemonCategory);

            _context.Add(pokemon);
        
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if(review.Count() <= 0)
                return 0;
            

            return ((decimal)review.Sum(r => r.Rating)/ review.Count());
            
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();

        }

        public Pokemon GetPokemonTrimToUpped(PokemonDto pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool PokemonExists(int pokeID)
        {
            return _context.Pokemons.Any(p=>p.Id == pokeID);    
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
        

        
    }
}
