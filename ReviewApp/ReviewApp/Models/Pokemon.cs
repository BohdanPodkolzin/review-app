namespace ReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Review> Reviews { get; } = new List<Review>();

        public List<PokemonOwner> PokemonOwner { get; } = [];
        public List<Owner> Owners { get; } = [];

        public List<PokemonCategories> PokemonCategories { get; } = [];
        public List<Category> Categories { get; } = [];
    }
}
