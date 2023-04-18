namespace mealSharingFfinal.reservation.Reservation;
using System.Text.Json.Serialization;
public class Reservation
{
  public int ID { get; set; }
  [JsonPropertyName("number_of_guests")]
  public int number_of_guests { get; set; }
  [JsonPropertyName("created_date")]
  public DateTime created_date { get; set; }

  [JsonPropertyName("contact_phonenumber")]
  public string contact_phonenumber { get; set; }

  [JsonPropertyName("contact_name")]
  public string contact_name { get; set; }

  [JsonPropertyName("contact_email")]
  public string contact_email { get; set; }

  [JsonPropertyName("meal_id")]
  public int meal_id { get; set; }
  public Reservation() { }
  public Reservation(int id, int numberOfGuests, DateTime createdDate, string contactPhoneNumber, string contactName, string contactEmail, int mealID)
  {
    ID = id;
    number_of_guests = numberOfGuests;
    created_date = createdDate;
    contact_phonenumber = contactPhoneNumber;
    contact_name = contactName;
    contact_email = contactEmail;
    meal_id = mealID;
  }
}
