using ReviewApp.Context;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Repository
{
    public class OwnerRepository(PokemonContext context) : IOwnerRepository
    {
        private readonly PokemonContext _context = context;

        public ICollection<Owner> GetOwners()
            => _context.Owners.OrderBy(x => x.Id).ToList();

        public Owner? GetOwner(int ownerId)
            => _context.Owners.SingleOrDefault(o => o.Id == ownerId);

        public ICollection<Owner> GetOwnersOfPokemon(int pokeId)
            => _context.PokemonOwners
                .Where(po => po.PokemonId == pokeId)
                .Select(po => po.Owner)
                .ToList();

        public ICollection<Pokemon> GetPokemonsByOwner(int ownerId)
            => _context.PokemonOwners
                .Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon)
                .ToList();

        public bool IsOwnerExists(int id)
            => _context.Owners.Any(x => x != null && x.Id == id);
    }
}
