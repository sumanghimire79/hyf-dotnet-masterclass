namespace mealSharingFfinal.meal.MealRepository;
using Dapper;
using MySql.Data.MySqlClient;
using mealSharingFfinal.meal.meal;
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
    var meals = await connection.QueryAsync<Meal>("SELECT * FROM meal");
    return meals;
  }
  public async Task<Object> GetMealByID(int id)
  {
    using var connection = new MySqlConnection(connectionString);
    var mealById = await connection.QueryAsync($"SELECT * FROM meal WHERE id = @id", new { id });
    return mealById;
  }
  public async Task<Meal> AddMeal(Meal meal)
  {
    await using var connection = new MySqlConnection(connectionString);
    var meaiId = await connection.ExecuteAsync("INSERT INTO meal (title, description, location, `when` , max_reservations, price, created_date) VALUES (@title, @description, @location, @when, @maxReservations, @price, @createdDate)", meal);
    return meal;
  }
  public async Task<Meal> UpdateMealByID(Meal meal, int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var productUpdated = await connection.ExecuteAsync($"UPDATE meal SET title=@title, description=@description, location=@location, `when`=@when, max_reservations=@maxReservations, price=@price, created_date=@createdDate  WHERE id={id}", meal);
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
    var searchMeal = await connection.QueryAsync<Meal>($"SELECT * FROM meal WHERE title LIKE '%{title}%'");
    return searchMeal;
  }
}
