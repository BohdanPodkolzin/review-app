namespace ReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Review> Reviews { get; set; } = [];

        public List<PokemonOwner> PokemonOwner { get; set;  } = [];
        public List<Owner> Owners { get; } = [];

        public List<PokemonCategory> PokemonCategories { get; set; } = [];
        public List<Category> Categories { get; } = [];
    }
}
