# Examples

## MVC Application with persons

```csharp
namespace MyMvcApplication.Models;

public class Person{
    public int ID { get; set; }

    public string Name { get; set; } 

    public int Age  { get; set; }

    public Gender Gender { get; set; }

}

public enum Gender{
    Male,
    Female,
    NonBinary
}

//HomeController:
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApplication.Models;

namespace MyMvcApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public List<Person> Persons { get; set; }=new List<Person>(){ new Person(){ Name="Allan", Age=42, Gender=Gender.Male, ID=1}};

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //return Json(Persons);
        return View(Persons);
    }

    public IActionResult Details(int ID){
        var person=Persons.FirstOrDefault(p => p.ID==ID);
        return Content($"Name: {person.Name} ({person.Age} years old)");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

```

## Web API with car

```csharp
namespace MyApplication.Models;

public class Car{
    public string Brand { get; set; }
    public string Model {get;set;}

    public int Year {get;set;}

    public int ID {get;set;}

    
}

//Car Controller
using Microsoft.AspNetCore.Mvc;
using MyApplication.Models;

namespace MyApplication.Controllers;


[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase{

    public static List<Car> Cars { get; set; } = new List<Car>(){
        new Car(){ ID=0, Brand="Fiat", Model="500", Year=2019},
        new Car(){ID=1, Brand="Volvo", Model="S90", Year=2022}
    };

    [HttpGet("GetAllCars")]
    public IEnumerable<Car> GetAllCars(){
        return Cars;
    }

    [HttpPost("AddCar")]
    public void AddCar([FromBody]Car car){
        Cars.Add(car);
    }

}
```
