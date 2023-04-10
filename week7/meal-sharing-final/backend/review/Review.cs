
namespace mealSharingFfinal.review.review;
using System.Text.Json.Serialization;
public class Review
{
  public int ID { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public int? Stars { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime CreatedDate { get; set; }
  [JsonPropertyName("meal_id")]
  public int MealID { get; set; }
  public Review() { }
  public Review(int id, string title, string description, int stars, DateTime createdDate, int mealID)
  {
    ID = id;
    Title = title;
    Description = description;
    Stars = stars;
    CreatedDate = createdDate;
    MealID = mealID;
  }
}
