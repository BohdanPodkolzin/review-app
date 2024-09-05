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
    }
}
