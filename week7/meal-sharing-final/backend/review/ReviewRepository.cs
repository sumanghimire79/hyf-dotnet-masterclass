namespace mealSharingFfinal.review.ReviewRepository;
using Dapper;
using MySql.Data.MySqlClient;
using mealSharingFfinal.review.review;
using mealSharingFfinal.review.IReviewRepository;

public class ReviewRepository : IReviewRepository
{
  private string connectionString;
  public ReviewRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }
  public async Task<IEnumerable<Review>> GetAllReview()
  {
    await using var connection = new MySqlConnection(connectionString);
    var reviews = await connection.QueryAsync<Review>("SELECT * FROM review");
    return reviews;
  }


  public async Task<Object> GetReviewByID(int id)
  {
    using var connection = new MySqlConnection(connectionString);
    var reviewById = await connection.QueryAsync($"SELECT * FROM review WHERE id = @id", new { id });
    return reviewById;
  }
  public async Task<Review> AddReview(Review review)
  {
    await using var connection = new MySqlConnection(connectionString);
    var reviewId = await connection.ExecuteAsync("INSERT INTO review (title, description, stars, created_date, meal_id) VALUES (@title, @description,@stars, @createdDate,@mealID)", review);
    return review;
  }
  public async Task<Review> UpdateReviewByID(Review review, int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var reviewUpdated = await connection.ExecuteAsync($"UPDATE review SET title=@title, description=@description, stars=@stars, created_date=@createdDate,meal_id=@mealID  WHERE id={id}", review);
    return review;
  }
  public async Task<Object> DeleteReviewByID(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var deletedReview = await connection.ExecuteAsync("DELETE FROM review WHERE id=@id", new { ID = id });
    return deletedReview;
  }
  public async Task<IEnumerable<Review>> SearchReview(string title)
  {
    await using var connection = new MySqlConnection(connectionString);
    var searchReview = await connection.QueryAsync<Review>($"SELECT * FROM review WHERE title LIKE '%{title}%'");
    return searchReview;
  }


}
