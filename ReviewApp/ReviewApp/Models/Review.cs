namespace ReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public int ReviewId { get; set; }
        public Reviewer Reviewer { get; set; } = null!;

        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; } = null!;

    }
}
