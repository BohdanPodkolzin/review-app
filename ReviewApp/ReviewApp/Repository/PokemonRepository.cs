using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewApp.Context;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class PokemonRepository(PokemonContext context) : IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons()
            => context.Pokemons.OrderBy(p => p.Id).ToList();

        public Pokemon? GetPokemon(int id)
            => context.Pokemons.SingleOrDefault(pokemon => pokemon.Id == id);

        public Pokemon? GetPokemon(string name)
            => context.Pokemons.FirstOrDefault(pokemon => pokemon.Name == name);

        public decimal GetPokemonRating(int pokemonId)
        {
            var reviews = context.Reviews.Where(review => review.PokemonId == pokemonId);
            
            if (!reviews.Any()) return 0;

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public bool PokemonExists(int pokemonId)
            => context.Pokemons.Any(pokemon => pokemon != null && pokemon.Id == pokemonId);
    }
}
