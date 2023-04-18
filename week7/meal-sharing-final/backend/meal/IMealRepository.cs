namespace mealSharingFfinal.meal.IMealRepository;
using mealSharingFfinal.meal.Meal;

public interface IMealRepository
{
  Task<IEnumerable<Meal>> GetAllMeal();
  Task<IEnumerable<Meal>> GetMealByID(int id);// IEnumerable because it needed array to map in frontend 
  Task<Meal> AddMeal(Meal meal);
  Task<Meal> UpdateMealByID(Meal meal, int id);
  Task<Object> DeleteMealByID(int id);
  Task<IEnumerable<Meal>> SearchMeal(string title);
  Task<IEnumerable<Object>> AvailableReservation();
  Task<IEnumerable<Object>> PopularMeal();
}