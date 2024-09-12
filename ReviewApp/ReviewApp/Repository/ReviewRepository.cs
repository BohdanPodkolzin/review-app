using ReviewApp.Context;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Repository
{
    public class ReviewRepository(PokemonContext context) : IReviewRepository
    {
        private readonly PokemonContext _context = context;

        public ICollection<Review> GetReviews()
            => _context.Reviews.OrderBy(r => r.Id).ToList();

        public Review? GetReview(int id)
            => _context.Reviews.SingleOrDefault(r => r != null && r.Id == id);

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
            => _context.Reviews
                .Where(r => r.ReviewerId == reviewerId)
                .OrderBy(r => r.Id)
                .ToList();

        public ICollection<Review> GetReviewsOfPokemon(int pokemonId)
            => _context.Reviews
                .Where(r => r.PokemonId == pokemonId)
                .OrderBy(r => r.Id)
                .ToList();

        public bool IsReviewExists(int id)
            => _context.Reviews.Any(r => r != null && r.Id == id);

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool Save()
            => _context.SaveChanges() > 0;
    }
}
