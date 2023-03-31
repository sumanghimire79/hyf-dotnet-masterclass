
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapGet("/", () => "week5-mealsharing dotnet");

/*
 <summary>
 Endpoint for adding a new blog post.
 <example>
 postman input
 <code>
 {
     "headline": "Nepal Hot Pot",
         "imageURL": "https://askme.com",
         "bodyText": "popular dumpling",
         "location": "ishoj,cph",
         "price": 500
 }
 </code>
 </example>
 </summary>
*/
app.MapPost("/meals", ([FromServices] IMealService mealSharingService, Meal meal) =>
{
  mealSharingService.AddMeal(meal);
});

/*
 <summary>
 Endpoint for getting all blog posts.
 <example>
 postman input
 <code>
 http://localhost:5157/meals
 </code>
 </example>
 </summary>
*/
app.MapGet("/meals", ([FromServices] IMealService mealSharingService) =>
{
  return mealSharingService.ListMeals();
});

/*
 <summary>
 Endpoint for updating a blog posts by id.
 <example>
 postman input
 <code>
 http://localhost:5157/meals/1
 </code>
 </example>
 </summary>
*/
app.MapPut("/meals/{id}", ([FromServices] IMealService mealSharingService, int id, Meal updateBody) =>
{
  mealSharingService.UpdateMeal(id, updateBody);
});

/*
 <summary>
 Endpoint for deletting a blog posts by id.
 <example>
 postman input
 <code>
 http://localhost:5157/meals/1
 </code>
 </example>
 </summary>
*/
app.MapDelete("/meals/{id}", ([FromServices] IMealService mealSharingService, int id) =>
{
  mealSharingService.DeleteMeal(id);
});

app.Run();

public interface IMealService
{
  List<Meal> ListMeals();
  void AddMeal(Meal meal);
  void DeleteMeal(int id);
  void UpdateMeal(int id, Meal updateBody);
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
  public void AddMeal(Meal meal)
  {
    if (!File.Exists("meals.json"))
    {
      File.WriteAllText("meals.json", "[]");
    }
    var readJsonFile = File.ReadAllText(@"meals.json");
    var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    // listOfMeals.Add(meal);
    var guid = Guid.NewGuid();
    var ids = guid.ToString();
    if (meals.Count == 0)
    {
      meal.ID = ids.Max(id => id);
    }
    else
    {
      foreach (var m in meals)
      {
        meal.ID = meals.Max(m => m.ID + 1);
      }
    }
    meals.Add(meal);
    var mealsJson = System.Text.Json.JsonSerializer.Serialize(meals);
    File.WriteAllText("meals.json", mealsJson);
  }
  public List<Meal> ListMeals()
  {
    var readJsonFile = File.ReadAllText(@"meals.json");
    var mealsFromJsonFile = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    // return listOfMeals;
    return mealsFromJsonFile;
  }
  public void UpdateMeal(int id, Meal updateBody)
  {
    var readJsonFile = File.ReadAllText(@"meals.json");
    var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    var mealtoUpdate = meals.Find(findMealtoUpdate => findMealtoUpdate.ID == id);
    mealtoUpdate.BodyText = updateBody.BodyText;
    mealtoUpdate.Headline = updateBody.Headline;
    mealtoUpdate.Price = updateBody.Price;
    mealtoUpdate.ImageURL = updateBody.ImageURL;
    var mealsJson = System.Text.Json.JsonSerializer.Serialize(meals);
    File.WriteAllText("meals.json", mealsJson);
  }
  public void DeleteMeal(int id)
  {
    var readJsonFile = File.ReadAllText(@"meals.json");
    var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(readJsonFile);
    meals.RemoveAll(findMealtoRemove => findMealtoRemove.ID == id);
    var mealsJson = System.Text.Json.JsonSerializer.Serialize(meals);
    File.WriteAllText("meals.json", mealsJson);
    Console.WriteLine(mealsJson);
  }
}