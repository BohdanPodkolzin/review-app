using ReviewApp.Context;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Repository
{
    public class ReviewerRepository(PokemonContext pokemonContext) : IReviewerRepository
    {
        private readonly PokemonContext _context = pokemonContext;

        public ICollection<Reviewer> GetReviewers()
            => _context.Reviewers.OrderBy(r => r.Id).ToList();

        public Reviewer? GetReviewer(int id)
            => _context.Reviewers.SingleOrDefault(x => x != null && x.Id == id);

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
            => _context.Reviews
                .Where(r => r.ReviewerId == reviewerId)
                .OrderBy(r => r.Id)
                .ToList();
        public bool IsReviewerExists(int id)
            => _context.Reviewers.Any(r => r != null && r.Id == id);

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }

        public bool Save()
            => _context.SaveChanges() > 0;
    }
}