## How to setup your project

Inside your .NET API project do the following:

1. Inside of the dotnet project folder create a new folder named `wwwroot`.
2. **[REACT ONLY]** Create a new folder named `ClientApp` folder. Copy code from `meal-sharing` app into it.
3. **[OPTIONAL][react only]** From `ClientApp` folder delete the following folders: `dist`, `node_modules`,`src\backend`.
4. **[REACT ONLY]** Edit `webpack.config.js` and change the `outputDirectory` to the following:
   ```javascript
   const outputDirectory = "../wwwroot";
   ```
5. **[REACT ONLY]** for easier debugging change `package.json` build target to development:
   ```javascript
   "build": "webpack --mode development",
   ```
6. **[REACT ONLY]** make sure you have `.babelrc` in the `ClientApp` folder. If it doesn't exist create one with following content:

```javascript
  {
    "presets": ["@babel/preset-env", "@babel/preset-react"],
    "plugins": [
      "@babel/plugin-transform-runtime",
      "@babel/plugin-proposal-class-properties"
    ]
  }
```

6. **[jQuery ONLY]** copy content of frontend into `wwwroot` folder
7. In `Program.cs` add the following lines before `app.MapControllers()`:

   ```csharp
    app.UseDefaultFiles();

    app.UseStaticFiles();

    app.MapFallbackToFile("index.html");
   ```

8. To make your backend compatible with your frontend we need to change route of our controllers, for example:

- `meal` -> `api/meals`
- `revision` -> `api/revisions`
- `review` -> `api/reviews`

  ```csharp
  [Route("api/meals")] // <-- change route
  public class MealsController : ControllerBase
  {
      ...
  }
  ```

8. All controler actions except for `get by id` should use default template. Same goes for `Reservations` and `Reviews`.

```csharp
public class MealController : ControllerBase
{
  [HttpGet("")]
  public async Task<IEnumerable<Meal>> GetMeals()

  [HttpPost("")]
  public async Task AddMeal([FromBody] Meal meal)

  [HttpGet("{id}")]
  public async Task <Meal> GetMeal(int id)
}
```

9. When inserting (adding) Meal:

   - make sure that you are passing appropriate fields. `Id` is auto generated and should not be passed
   - If you are not passing `When` from the UI, remove it from the Insert statement (`When` should be defined as nullable in the DB)
   - `created_date` should be set current date/time. Assign `DateTime.Now` to `Meal.CreateDate`

   ```csharp
       public class Model
       {
         ...
         public DateTime Created_date { get; set; } = DateTime.Now;
         ...
       }
   ```

10. Make search by title.

    - first we need to introduce a new model, call it MealSearch and add Title (string) property into it

    ```csharp
    public class MealSearch
    {
      public string Title { get; set; }
    }
    ```

    - add this new model as parameter to `get all meals` endpoint

    ```csharp
    public async Task<IEnumerable<Meal>> GetMeals([FromQuery]MealSearch mealSearch)
    ```

    - Perform search by using sql LIKE operation:

    ```csharp
        public async Task<IEnumerable<Meal>> ListMeals(MealSearch mealSearch)
        {
            var query = "select * from meals";

            if(!string.IsNullOrWhiteSpace(mealSearch?.Title))
            {
                query+=" where title like @title";
            }

            var meals = await _connection.QueryAsync<Meal>(query, new{ Title = "%" + mealSearch.Title + "%"});

            return meals;
        }
    ```

or loading all meals & searching in memory (done by some in their nodejs project):

```csharp
    public async Task<IEnumerable<Meal>> ListMeals(MealSearch mealSearch)
    {
        var meals = await _connection.QueryAsync<Meal>("select * from meals");

        if(!string.IsNullOrWhiteSpace(mealSearch?.Title))
        {
            return meals.Where(meal => meal.Title.Contains(mealSearch.Title));
        }

        return meals;
    }
```

## How to run:

1. from the `ClientApp` folder run `npm i` or `npm i --force` if webpack conflicts arise and then `npm run build`.
2. run backend from VSCode with F5 or using `dotnet watch run` or `dotnet run`
