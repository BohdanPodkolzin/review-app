using ReviewApp.Models;

namespace ReviewApp.Service
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner? GetOwner(int ownerId);

        ICollection<Owner> GetOwnersOfPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonsByOwner(int ownerId);

        bool IsOwnerExists(int id);

        bool CreateOwner(int pokemonId, Owner owner);
        protected bool Save();
    }
}
