
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
builder.Services.Remove(ServiceDescriptor.Transient<IMealService, FileMealService>());//delete meal service

var app = builder.Build();

app.MapGet("/", () => "week5-mealsharing dotnet");

app.MapPost("/meals", ([FromServices] IMealService mealSharingService, Meal meal) =>
{/*eg: postman input
  {
    "headline": "Nepal Hot Pot",
        "imageURL": "https://askme.com",
        "bodyText": "popular dumpling",
        "location": "ishoj,cph",
        "price": 500
}*/
  mealSharingService.AddMeal(meal);
});

app.MapGet("/meals", ([FromServices] IMealService mealSharingService) =>
{
  return mealSharingService.ListMeals();
});
app.MapDelete("/meals", ([FromServices] IMealService mealSharingService, int id) =>
{
  mealSharingService.DeleteMeal(id);
});
app.Run();

public interface IMealService
{
  List<Meal> ListMeals();
  void AddMeal(Meal meal);
  void DeleteMeal(int id);
}
public class Meal
{
  public int ID { get; set; }
  public string? Headline { get; set; }
  public string? ImageURL { get; set; }
  public string? BodyText { get; set; }
  public string? Location { get; set; }
  public int Price { get; set; }
}
public class FileMealService : IMealService
{
  // public List<Meal> listOfMeals = new List<Meal>();
  public List<Meal> ListMeals()
  {
    var readJsonFile = File.ReadAllText(@"meals.json");
    var mealsFromJsonFile = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    // return listOfMeals;
    return mealsFromJsonFile;
  }
  public void AddMeal(Meal meal)
  {
    if (!File.Exists("meals.json"))
    {
      File.WriteAllText("meals.json", "[]");
    }
    var readJsonFile = File.ReadAllText(@"meals.json");
    var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    // listOfMeals.Add(meal);
    if (meals.Count == 0)
    {
      meal.ID = 1;
    }
    if (meal.ID == 0)
    {
      meal.ID = meals[meals.Count - 1].ID + 1;
    }
    foreach (var m in meals)
    {
      if (m.ID == meal.ID)
      {
        throw new Exception($"{meal.ID} already exists");
      }
      else if (m.ID != m.ID)
      {
        meal.ID = meals[meals.Count - 1].ID + 1;
      }
    }
    meals.Add(meal);
    var mealsJson = System.Text.Json.JsonSerializer.Serialize(meals);
    File.WriteAllText("meals.json", mealsJson);
    Console.WriteLine(mealsJson);
  }

  public void DeleteMeal(int id)
  {
    var readJsonFile = File.ReadAllText(@"meals.json");
    var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    var toRemove = new List<Meal>();
    foreach (Meal meal in meals)
    {
      if (meal.ID == id) { toRemove.Add(meal); }
    }
    meals.RemoveAll(meal => meal.ID == id);
    var mealsJson = System.Text.Json.JsonSerializer.Serialize(meals);
    File.WriteAllText("meals.json", mealsJson);
    Console.WriteLine(mealsJson);
  }

}