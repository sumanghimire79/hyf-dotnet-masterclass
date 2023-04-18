
namespace mealSharingFfinal.reservation.reservation;
using System.Text.Json.Serialization;
public class Reservation
{
  public int ID { get; set; }
  [JsonPropertyName("number_of_guests")]
  public int NumberOfGuests { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime CreatedDate { get; set; }

  [JsonPropertyName("contact_phonenumber")]
  public string ContactPhoneNumber { get; set; }

  [JsonPropertyName("contact_name")]
  public string ContactName { get; set; }

  [JsonPropertyName("contact_email")]
  public string ContactEmail { get; set; }

  [JsonPropertyName("meal_id")]
  public int MealID { get; set; }
  public Reservation(int id, int numberOfGuests, DateTime createdDate, string contactPhoneNumber, string contactName, string contactEmail, int mealID)
  {
    ID = id;
    NumberOfGuests = numberOfGuests;
    CreatedDate = createdDate;
    ContactPhoneNumber = contactPhoneNumber;
    ContactName = contactName;
    ContactEmail = contactEmail;
    MealID = mealID;
  }
}
