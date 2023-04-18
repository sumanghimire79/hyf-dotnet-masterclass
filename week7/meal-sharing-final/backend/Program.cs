
using mealSharingFfinal.meal.Meal;
using mealSharingFfinal.meal.MealRepository;
using mealSharingFfinal.meal.IMealRepository;
using mealSharingFfinal.review.Review;
using mealSharingFfinal.review.ReviewRepository;
using mealSharingFfinal.review.IReviewRepository;
using mealSharingFfinal.reservation.Reservation;
using mealSharingFfinal.reservation.ReservationRepository;
using mealSharingFfinal.reservation.IReservationRepository;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
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
  // return await mealRepository.AddMeal(meal);
  var createdMeal = await mealRepository.AddMeal(meal);
  return Results.Created($"/api/mealsy/{createdMeal.ID}", createdMeal);

});

app.MapPut("/api/meals/{id}", async (IMealRepository mealRepository, Meal meal, int id) =>
{
  return await mealRepository.UpdateMealByID(meal, id);
});

app.MapDelete("/api/meals/{id}", async (IMealRepository mealRepository, int id) =>
{
  return await mealRepository.DeleteMealByID(id);
});

app.MapGet("/api/meals/availableReservations", async (IMealRepository mealRepository) =>
{
  return await mealRepository.AvailableReservation();
});
app.MapGet("/api/meals/popularmeal", async (IMealRepository mealRepository) =>
{
  return await mealRepository.PopularMeal();
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
  // return await reviewRepository.AddReview(review);

  var createdReview = await reviewRepository.AddReview(review);
  return Results.Created($"/api/reviews/{createdReview.ID}", createdReview);
});

app.MapPut("/api/reviews/{id}", async (IReviewRepository reviewRepository, Review review, int id) =>
{
  return await reviewRepository.UpdateReviewByID(review, id);
});

app.MapDelete("/api/reviews/{id}", async (IReviewRepository reviewRepository, int id) =>
{
  return await reviewRepository.DeleteReviewByID(id);
});

//reservation routes
app.MapGet("/api/reservations", async (IReservationRepository reservationRepository) =>
{
  return await reservationRepository.GetAllReservation();
});

app.MapGet("/api/reservations/search/", async (IReservationRepository reservationRepository, string title) =>
{
  return await reservationRepository.SearchReservation(title);
});

app.MapGet("/api/reservations/{id}", async (IReservationRepository reservationRepository, int id) =>
{
  return await reservationRepository.GetReservationByID(id);
});

app.MapPost("/api/reservations", async (IReservationRepository reservationRepository, Reservation reservation) =>
{
  var createdReservation = await reservationRepository.AddReservation(reservation);
  return Results.Created($"/api/reservations/{createdReservation.ID}", createdReservation);
});

app.MapPut("/api/reservations/{id}", async (IReservationRepository reservationRepository, Reservation reservation, int id) =>
{

  return await reservationRepository.UpdateReservationByID(reservation, id);
});

app.MapDelete("/api/reservations/{id}", async (IReservationRepository reservationRepository, int id) =>
{
  return await reservationRepository.DeleteReservationByID(id);
});

app.Run();

