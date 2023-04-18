
namespace mealSharingFfinal.review.Review;
using System.Text.Json.Serialization;
public class Review
{
  public int ID { get; set; }
  public string? title { get; set; }
  public string? description { get; set; }
  public int? stars { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime created_date { get; set; }
  [JsonPropertyName("meal_id")]
  public int meal_id { get; set; }
  public Review() { }
  public Review(int id, string title, string description, int stars, DateTime createdDate, int mealID)
  {
    ID = id;
    this.title = title;
    this.description = description;
    this.stars = stars;
    created_date = createdDate;
    meal_id = mealID;
  }
}
