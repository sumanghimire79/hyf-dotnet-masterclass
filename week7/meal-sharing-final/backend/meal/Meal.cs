
namespace mealSharingFfinal.meal.meal;
using System.Text.Json.Serialization;
public class Meal
{
  public int ID { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string? Location { get; set; }
  public DateTime When { get; set; }
  [JsonPropertyName("max_reservations")]
  public int MaxReservations { get; set; }
  public decimal? Price { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime CreatedDate { get; set; }
  public Meal() { }
  public Meal(int id, string title, string description, string location, DateTime when, int maxReservations, decimal price, DateTime createdDate)
  {
    ID = id;
    Title = title;
    Description = description;
    Location = location;
    When = when;
    MaxReservations = maxReservations;
    Price = price;
    CreatedDate = createdDate;
  }
}
