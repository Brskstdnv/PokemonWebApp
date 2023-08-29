using PokemonWebApp.Dto;
using PokemonWebApp.Models;

namespace PokemonWebApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons(); 
        Pokemon GetPokemon(int id); 
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeID);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        Pokemon GetPokemonTrimToUpped(PokemonDto pokemonCreate);
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();
    }
    
       
}
