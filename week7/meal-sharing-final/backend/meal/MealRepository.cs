namespace mealSharingFfinal.meal.MealRepository;
using Dapper;
using MySql.Data.MySqlClient;
using mealSharingFfinal.meal.Meal;
using mealSharingFfinal.meal.IMealRepository;



public class MealRepository : IMealRepository
{
  private string connectionString;
  public MealRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }
  public async Task<IEnumerable<Meal>> GetAllMeal()
  {
    await using var connection = new MySqlConnection(connectionString);
    var meals = await connection.QueryAsync<Meal>($"SELECT * FROM meal");
    return meals;
  }

  public async Task<IEnumerable<Meal>> GetMealByID(int id)
  {
    using var connection = new MySqlConnection(connectionString);
    var mealById = await connection.QueryAsync<Meal>($"SELECT * FROM meal WHERE id = @id", new { id });
    return mealById;
  }
  public async Task<Meal> AddMeal(Meal meal)
  {
    await using var connection = new MySqlConnection(connectionString);
    var addMeal = await connection.QuerySingleAsync<int>(@"INSERT INTO meal (title, description, location, `when` , max_reservations, price, created_date) VALUES (@title, @description, @location, @when, @max_reservations, @price, @created_date );
     SELECT LAST_INSERT_ID();
     ", meal);
    var createdMealById = await connection.QueryFirstAsync<Meal>($"SELECT * FROM meal WHERE id = @addMeal", new { addMeal });
    return createdMealById;
  }
  public async Task<Meal> UpdateMealByID(Meal meal, int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var productUpdated = await connection.ExecuteAsync($"UPDATE meal SET title=@title, description=@description, location=@location, `when`=@when, max_reservations=@max_reservations, price=@price, created_date=@created_date  WHERE id={id}", meal);
    return meal;
  }
  public async Task<Object> DeleteMealByID(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var deletedProduct = await connection.ExecuteAsync("DELETE FROM meal WHERE id=@id", new { ID = id });
    return deletedProduct;
  }
  public async Task<IEnumerable<Meal>> SearchMeal(string title)
  {
    await using var connection = new MySqlConnection(connectionString);
    var searchMeal = await connection.QueryAsync<Meal>($"SELECT `id`, `title`, `description`, `location`,`when`,`max_reservations` as maxReservations, `price`,`created_date` FROM meal WHERE title LIKE '%{title}%'");
    return searchMeal;
  }

  //available reservation 
  public async Task<IEnumerable<Object>> AvailableReservation()
  {
    await using var connection = new MySqlConnection(connectionString);
    var availableReservation = await connection.QueryAsync<Object>($"SELECT meal.id, meal.title, max_reservations, SUM(number_of_guests) as total_guests , (max_reservations-SUM(number_of_guests)) AS Available_Reservation FROM meal inner join reservation on meal.id = reservation.meal_id where max_reservations > number_of_guests GROUP BY meal_id having (max_reservations-SUM(number_of_guests)) > 0");
    return availableReservation;
  }
  public async Task<IEnumerable<Object>> PopularMeal()
  {
    await using var connection = new MySqlConnection(connectionString);
    var availableReservation = await connection.QueryAsync<Object>($"SELECT meal.id, meal.title, meal.location, meal.price , review.stars from meal inner join review on meal.id= review.meal_id where review.stars > 3 GROUP BY meal_id");
    return availableReservation;
  }


}
