using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewApp.Context;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class PokemonRepository(PokemonContext context) : IPokemonRepository
    {
        private readonly PokemonContext _context = context;
        public ICollection<Pokemon> GetPokemons()
            => _context.Pokemons.OrderBy(p => p.Id).ToList();

        public Pokemon? GetPokemon(int id)
            => _context.Pokemons.SingleOrDefault(pokemon => pokemon.Id == id);

        public Pokemon? GetPokemon(string name)
            => _context.Pokemons.FirstOrDefault(pokemon => pokemon.Name == name);

        public decimal GetPokemonRating(int pokemonId)
        {
            var reviews = _context.Reviews.Where(review => review.PokemonId == pokemonId);
            
            if (!reviews.Any()) return 0;

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public bool PokemonExists(int pokemonId)
            => _context.Pokemons.Any(pokemon => pokemon != null && pokemon.Id == pokemonId);

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.SingleOrDefault(x => x != null && x.Id == ownerId);

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            _context.Add(pokemonOwner);

            var pokemonCategoryEntity = _context.Categories.SingleOrDefault(x => x != null && x.Id == categoryId);

            var pokemonCategory = new PokemonCategory()
            {
                Category = pokemonCategoryEntity,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }


        public bool Save()
            => _context.SaveChanges() > 0;
    }
}
