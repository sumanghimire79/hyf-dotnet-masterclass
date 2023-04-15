namespace mealSharingFfinal.review.IReviewRepository;
using mealSharingFfinal.review.Review;
public interface IReviewRepository
{
  Task<IEnumerable<Review>> GetAllReview();
  Task<Object> GetReviewByID(int id);
  Task<Review> AddReview(Review review);
  Task<Review> UpdateReviewByID(Review review, int id);
  Task<Object> DeleteReviewByID(int id);
  Task<IEnumerable<Review>> SearchReview(string title);
}