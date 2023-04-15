namespace mealSharingFfinal.meal.Meal;
using System.Text.Json.Serialization;
public class Meal
{
  public int ID { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public string? Location { get; set; }
  public DateTime When { get; set; }
  [JsonPropertyName("max_reservations")]
  public int? max_reservations { get; set; }
  public decimal? Price { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime? created_date { get; set; }
  public Meal() { }
  public Meal(int id, string title, string description, string location, DateTime when, int maxReservations, decimal price, DateTime createdDate)
  {
    ID = id;
    Title = title;
    Description = description;
    Location = location;
    When = when;
    max_reservations = maxReservations;
    Price = price;
    created_date = createdDate;
  }
}
