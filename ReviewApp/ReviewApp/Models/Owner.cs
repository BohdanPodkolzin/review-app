namespace ReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gym { get; set; }


        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        public List<PokemonOwner> PokemonOwner { get; } = [];
        public List<Pokemon> Pokemons { get; } = [];
    }
}
