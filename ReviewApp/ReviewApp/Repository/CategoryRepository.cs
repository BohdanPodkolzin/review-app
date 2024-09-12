using ReviewApp.Context;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Repository
{
    public class CategoryRepository(PokemonContext context) : ICategoryRepository
    {
        public readonly PokemonContext _context = context;

        public ICollection<Category> GetCategories()
            => _context.Categories.OrderBy(c => c.Id).ToList();

        public Category? GetCategory(int id)
            => _context.Categories.SingleOrDefault(c => c != null && c.Id == id);

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId) 
            => _context.PokemonCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(c => c.Pokemon)
                .ToList();
        
        public bool IsCategoryExists(int id)
            => _context.Categories.Any(c => c != null && c.Id == id);

        public bool CreateCategory(int pokemonId, Category category)
        {
            var pokemonEntity = _context.Pokemons.SingleOrDefault(x => x != null && x.Id == pokemonId);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemonEntity
            };

            _context.Add(pokemonCategory);

            _context.Add(category);

            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool Save()
            => context.SaveChanges() > 0;
    }
}
