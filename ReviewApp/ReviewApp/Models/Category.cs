namespace ReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PokemonCategory> PokemonCategories { get; } = [];
        public List<Pokemon> Pokemons { get; } = [];
    }
}
