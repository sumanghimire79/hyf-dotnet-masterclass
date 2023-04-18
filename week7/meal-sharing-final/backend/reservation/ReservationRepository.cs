namespace mealSharingFfinal.reservation.ReservationRepository;
using Dapper;
using MySql.Data.MySqlClient;
using mealSharingFfinal.reservation.reservation;
using mealSharingFfinal.reservation.IReservationRepository;

public class ReservationRepository : IReservationRepository
{
  private string connectionString;
  public ReservationRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }
  public async Task<IEnumerable<Reservation>> GetAllReservation()
  {
    await using var connection = new MySqlConnection(connectionString);
    var reservations = await connection.QueryAsync<Reservation>("SELECT number_of_guests, created_date, contact_phonenumber, contact_name,contact_email, meal_id FROM reservation");
    return reservations;
  }


  public async Task<Object> GetReservationByID(int id)
  {
    using var connection = new MySqlConnection(connectionString);
    var reservationsById = await connection.QueryAsync($"SELECT * FROM reservation WHERE id = @id", new { id });
    return reservationsById;
  }
  public async Task<Reservation> AddReservation(Reservation reservation)
  {
    await using var connection = new MySqlConnection(connectionString);
    var addReservation = await connection.ExecuteAsync("INSERT INTO reservation (number_of_guests, created_date, contact_phonenumber, contact_name,contact_email, meal_id) VALUES (@NumberOfGuests, @CreatedDate,@ContactPhoneNumber, @ContactName,@ContactEmail,@mealID)", reservation);
    return reservation;
  }
  public async Task<Reservation> UpdateReservationByID(Reservation reservation, int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var reservationUpdated = await connection.ExecuteAsync($"UPDATE reservation SET number_of_guests=@NumberOfGuests, created_date=@CreatedDate, contact_phonenumber=@ContactPhoneNumber, contact_name=@ContactName,contact_email=@ContactEmail,meal_id=@mealID  WHERE id={id}", reservation);
    return reservation;
  }
  public async Task<Object> DeleteReservationByID(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var deletedReservation = await connection.ExecuteAsync("DELETE FROM reservation WHERE id=@id", new { ID = id });
    return deletedReservation;
  }
  public async Task<IEnumerable<Reservation>> SearchReservation(string title)
  {
    await using var connection = new MySqlConnection(connectionString);
    var searchReservation = await connection.QueryAsync<Reservation>($"SELECT * FROM reservation WHERE title LIKE '%{title}%'");
    return searchReservation;
  }


}
