# Examples for week 4

## Enum example
```csharp
public enum PowerType{
    Electric,
    Gasoline,
    Diesel
}

//Use PowerType as a property type.

```

## Interface example
We'll define an interface for any vehicle - like our Car class from the previous example.
Remember, an Interface is just the definition - or contract - of what signatures should be implemented by the classes that implement the interface.

```csharp
public interface IVehicle{
    int Seats{get;set;}

    PowerType Power{get;set;} //Notice how we are using the enum

    int Wheels{get;set;}
}

public interface IElectricPoweredVehicle{
    int BatterySizeInKwH{get;set;}
}

//And used here for Car
public class Car : IVehicle {
    
    public int ID { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public PowerType Power { get; set; }
    public int Year { get; set; }
    public int Seats { get; set; }
    public int Wheels { get; set; }
}

//And another Vehicle, a motorcycle:
public class Motorcycle : IVehicle
{
    public int Seats { get; set; } =2;
    public PowerType Power { get; set; }
    public int Wheels { get; set; }=2;
}

//And finally an electric car that both inherits a base class, but also implements the IElectricPoweredVehicle interface
public class ElectricCar : Car, IElectricPoweredVehicle
{
    public int BatterySizeInKwH {get;set;}
}

```


## Dependency Injection for Service classes in ASP.NET

In order to move business logic out of our controller and into service classes, we need to use Dependency Injection so it's easy later to switch which service class is used. This is both to let others use our code as a library and switch out parts to fit their needs (and hence improve the reusability) but also support things like Unit testing with mock objects.

```csharp
//First, we define an ICarRepository with the contract of what a car repository service should provice:
public interface ICarRepository{
     IEnumerable<Car> ListCars();

    void Add(Car c);

    void Delete(int ID);
}

//And then we can implement a simple 'dumb' version of it that just keeps the repository in an in-memory list:
public class MyCarRepository : ICarRepository{

    private List<Car> Cars { get; set; } = new List<Car>(){
        new Car(){Make="Fiat", Model="500", Year=2017, ID=0},
        new Car(){Make="Volvo", Model="S90", Year=2022,ID=1},
        new Car(){Make="Tesla", Model="Y", Year=2022, ID=2}
    };

    public IEnumerable<Car> ListCars(){
        return Cars;
    }

    public void Add(Car c){
        Cars.Add(c);
    }

    public void Delete(int ID){
                Cars.RemoveAll( c => c.ID==ID);
    }
}

//Then we need to register the class in our service collection (in this case done in program.cs):

builder.Services.AddSingleton<ICarRepository,MyCarRepository>();
//Note, we are registrering it as a Singleton, so it will stay in memory as opposed to Transient services that are created upon demand and then disposed.

//Finally, we can use it in our controller, by letting the repository be injected in the constructor:
[ApiController]
[Route("cars")]
public class CarController : ControllerBase{
    private ICarRepository _repo;

    public CarController(ICarRepository repo){
        _repo=repo;
    }

    [HttpGet("List")]
    public List<Car> ListAllCars(){

        return _repo.ListCars().ToList();
    }

    [HttpPost("Add")]
    public void AddCar([FromBody]Car c){
        _repo.Add(c);
    }

    [HttpDelete("Delete")]
    public void DeleteCar(int ID){
        _repo.Delete(ID);
    }

}

```