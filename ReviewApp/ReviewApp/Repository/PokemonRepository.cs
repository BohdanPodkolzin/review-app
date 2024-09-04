using ReviewApp.Context;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class PokemonRepository(PokemonContext context) : IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons()
            => context.Pokemons.OrderBy(p => p.Id).ToList();

    }
}
