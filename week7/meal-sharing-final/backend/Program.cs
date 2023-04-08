using Dapper;
// using meal_sharing_final.meal;
using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddScoped<IMealRepository, MealRepository>();

var app = builder.Build();

app.MapGet("/", () => "week 7 meal sharing final!");

// app.MapGet("/api/meals", () => new[] { new Meal(){ Title= "momo", MaxReservations = 111, Price=111

// }});
app.MapGet("/api/meals", async (IConfiguration configuration) =>
{
  // // starting from C# 8 you no longer need inner {}
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var meals = await connection.QueryAsync("SELECT * FROM meal");
  return meals;
  // return await mealRepository.GetAllMeal();

});


app.MapGet("/api/meals/{id}", async (IConfiguration configuration, int id) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var mealById = await connection.QueryAsync($"SELECT * FROM meal WHERE id = {id}");
  return mealById;
  // return Results.Ok(new
  // {
  //   Status = "200 ok",
  //   Message = $"The product that has id {id} is: ",
  //   ProductById = mealById,
  // });
});
app.MapPut("/api/meals/{id}", async (IConfiguration configuration, Meal meal, int id) =>
{
  Console.WriteLine(meal);
  Console.WriteLine("i was there hello");
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var productUpdated = await connection.ExecuteAsync($"UPDATE meal SET title=@title, description=@description location=@location when=@when max_reservations=@max_reservations price=@price created_date=@created_date  WHERE id={id}", meal);
  return meal;
});


app.MapPost("/api/meals", async (IConfiguration configuration, Meal meal) =>
{
  Console.WriteLine("sdfghjk");
  Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(meal));
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Defalult"));
  var meaiId = await connection.ExecuteAsync("INSERT INTO meal (title, description, location, when , `max_reservations`, price, `created_date`) VALUES (@title, @description, @location, @when, @maxReservations, @price, @createdDate)", meal);

  return meal;
});

app.MapDelete("/api/meals/{id}", async (IConfiguration configuration, int id) =>
{
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var deletedProduct = await connection.ExecuteAsync("DELETE FROM meal WHERE id=@id", new { ID = id });
  return Results.Ok(new
  {
    Status = "200 ok",
    Message = $"this product id:{id} is deleted",
    DeletedProduct = deletedProduct,
  });
});

app.Run();


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
  [JsonPropertyName("created__date")]
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

/*
public interface IMealRepository
{
  Task<IEnumerable<Meal>> GetAllMeal();
}
public class MealRepository : IMealRepository
{
  private string connectionString;

  public MealRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }
  public async Task<IEnumerable<Meal>> GetAllMeal()
  {
    // starting from C# 8 you no longer need inner {}
    await using var connection = new MySqlConnection(connectionString);
    var meals = await connection.QueryAsync<Meal>("SELECT * FROM meal");
    return meals;
  }
}
*/