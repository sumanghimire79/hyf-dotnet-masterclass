var meal = new Meal();
await AddMealAsync(meal);

async Task AddMealAsync(Meal meal)
{
    var repository = new DatabaseRepository();
    await repository.Insert(meal);
}

internal class DatabaseRepository
{
    public DatabaseRepository()
    {
    }

    internal Task Insert(Meal meal)
    {
        return Task.CompletedTask;
    }
}

public class Meal
{

}