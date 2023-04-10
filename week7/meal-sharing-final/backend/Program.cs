
using mealSharingFfinal.meal.meal;
using mealSharingFfinal.meal.MealRepository;
using mealSharingFfinal.meal.IMealRepository;
using mealSharingFfinal.review.review;
using mealSharingFfinal.review.ReviewRepository;
using mealSharingFfinal.review.IReviewRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

var app = builder.Build();

app.MapGet("/", () => "week 7 meal sharing final!");

//meal routes
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


//review routes
app.MapGet("/api/reviews", async (IReviewRepository reviewRepository) =>
{
  return await reviewRepository.GetAllReview();
});

app.MapGet("/api/reviews/search/", async (IReviewRepository reviewRepository, string title) =>
{
  return await reviewRepository.SearchReview(title);
});

app.MapGet("/api/reviews/{id}", async (IReviewRepository reviewRepository, int id) =>
{
  return await reviewRepository.GetReviewByID(id);
});

app.MapPost("/api/reviews", async (IReviewRepository reviewRepository, Review review) =>
{
  return await reviewRepository.AddReview(review);
});

app.MapPut("/api/reviews/{id}", async (IReviewRepository reviewRepository, Review review, int id) =>
{
  return await reviewRepository.UpdateReviewByID(review, id);
});

app.MapDelete("/api/reviews/{id}", async (IReviewRepository reviewRepository, int id) =>
{
  return await reviewRepository.DeleteReviewByID(id);
});

app.Run();

