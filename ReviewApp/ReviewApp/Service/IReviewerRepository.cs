﻿using ReviewApp.Models;

namespace ReviewApp.Service
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer? GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);

        bool IsReviewerExists(int id);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReview(Reviewer reviewer);
        protected bool Save();
    }
}
