using ReviewApp.Models;

namespace ReviewApp.Service
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review?> GetReviewsByReviewer(int reviewerId);
        ICollection<Review> GetReviewsOfPokemon(int pokemonId);
        bool IsReviewExists(int id);

        bool CreateReview(Review review);
        protected bool Save();
    }
}
