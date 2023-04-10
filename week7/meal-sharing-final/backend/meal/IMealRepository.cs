namespace mealSharingFfinal.meal.IMealRepository;
using mealSharingFfinal.meal.meal;
public interface IMealRepository
{
  Task<IEnumerable<Meal>> GetAllMeal();
  Task<Object> GetMealByID(int id);
  Task<Meal> AddMeal(Meal meal);
  Task<Meal> UpdateMealByID(Meal meal, int id);
  Task<Object> DeleteMealByID(int id);
  Task<IEnumerable<Meal>> SearchMeal(string title);
}