
using mealSharingFfinal.meal.meal;
using mealSharingFfinal.meal.MealRepository;
using mealSharingFfinal.meal.IMealRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealRepository, MealRepository>();

var app = builder.Build();

app.MapGet("/", () => "week 7 meal sharing final!");

app.MapGet("/api/meals", async (IMealRepository mealRepository) =>
{
  return await mealRepository.GetAllMeal();
});

app.MapGet("/api/meals/search/", async (IMealRepository mealRepository, string title) =>
{
  return await mealRepository.SearchMeal(title);
});

app.MapGet("/api/meals/{id}", async (IMealRepository mealRepository, int id) =>
{
  return await mealRepository.GetMealByID(id);
});

app.MapPost("/api/meals", async (IMealRepository mealRepository, Meal meal) =>
{
  return await mealRepository.AddMeal(meal);
});

app.MapPut("/api/meals/{id}", async (IMealRepository mealRepository, Meal meal, int id) =>
{
  return await mealRepository.UpdateMealByID(meal, id);
});

app.MapDelete("/api/meals/{id}", async (IMealRepository mealRepository, int id) =>
{
  return await mealRepository.DeleteMealByID(id);
});

app.Run();

