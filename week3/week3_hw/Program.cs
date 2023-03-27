var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "week-3 homework!");

app.MapGet("/account", () =>
{
  var account = new Account();
  account.Deposit(100);
  Console.WriteLine($"Account balance is {account.Balance}");
  account.Withdraw(10);
  Console.WriteLine($"Account new balance is {account.Balance}");
  // account.Withdraw(200); // ❌ we should not be able to withdraw more than we have in the balance
  return $"Account remaining balance is {account.Balance}";
});

app.MapGet("/interface", () =>
{
  IAnimal dog = new Dog();
  dog.Name = "vote";
  dog.Sound = "big vou vou....";
  IAnimal cat = new Cat();
  cat.Name = "Rainbow cat";
  cat.Sound = "Mau Mau....";
  IAnimal cow = new Cow();
  cow.Name = "Holsten";
  cow.Sound = "Mu Mu Mu...";
  Console.WriteLine(MakeSound(dog));
  Console.WriteLine(MakeSound(cat));
  Console.WriteLine(MakeSound(cow));
  return MakeSound(dog) + MakeSound(cat) + MakeSound(cow);
});
string MakeSound(IAnimal animal)
{
  return $"{animal.Name} makes {animal.Sound}";
}
app.MapGet("/temperature", () =>
{
  var temperature = new Temperature(100.4m);
  Console.WriteLine($" {temperature.Celsius} degree centigrade is equal to : {temperature.GetFahrenheit()} degree farenheight");
  Console.WriteLine($" {temperature.Celsius} degree farenheight is equal to {temperature.GetCentigrade()} degree centigrade");
});
app.MapGet("/exchange", (double amount, double rate) =>
{
  var exchangeRate = new ExchangeRate("EUR", "DKK");
  exchangeRate.Rate = rate;
  return $"{amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}";
});
app.Run();

//1. cccount
// Create Account class that can be initialized with the amount value. 
// Account class contains Withdraw and Deposit methods and Balance (get only) property.
//  Make sure that you can't withdraw more than you have in the balance.
public class Account
{

  public double _balance;
  public double Balance { get => _balance; }
  public double Deposit(double amountToDeposit)
  {
    if (amountToDeposit < 0)
    {
      throw new Exception("negative amount can not be deposited");
    }
    double result = _balance + amountToDeposit;
    this._balance = result;
    return result;
  }
  public double Withdraw(double amountToWithdraw)
  {
    if (amountToWithdraw > _balance)
    {
      throw new Exception("you can not withdraw more than your balance");
    }
    double result = Balance - amountToWithdraw;
    this._balance = result;
    return result;
  }
}

//2. interface
// Create interface IAnimal with property Name and Sound . 
// Create classes Cow, Cat and Dog that implement IAnimal .
//  Instantiate all three classes and pass them to a new method 
//  called MakeSound that has single parameter IAnimal and it writes 
//  to console eg “Dog says woof woof” if instance of the Dog class is passed.

interface IAnimal
{
  string Name { get; set; }
  string Sound { get; set; }
}

class Cow : IAnimal
{
  public string? Name { get; set; }
  public string? Sound { get; set; }
}
class Cat : IAnimal
{
  public string? Name { get; set; }
  public string? Sound { get; set; }
}
class Dog : IAnimal
{

  public string? Name { get; set; }
  public string? Sound { get; set; }
}

// 3. exchange
// Create a class named ExchangeRate with a constructor with two string parameters, fromCurrency and toCurrency.
// Add a decimal property called Rate and method Calculate with decimal parameter amount return value of 
// the method should be a product of Rate and amount multiplication.

public class ExchangeRate
{
  public string FromCurrency { get; set; }
  public string ToCurrency { get; set; }
  public double _rate = 7.5;
  public double Rate
  {
    get => _rate; set
    {
      if (value < 0)
      {
        throw new Exception("rate can not be negative");
      }
      _rate = value;
    }
  }

  public ExchangeRate(string fromCurrency, string toCurrency)
  {
    FromCurrency = fromCurrency;
    ToCurrency = toCurrency;
  }
  public double Calculate(double amount)
  {
    if (amount < 0)
    {
      throw new Exception("amount can not be negative");
    }
    return amount * Rate;
  }
}

//4.temperature
//  Create a class named Temperature and make a constructor with one decimal parameter - degrees (Celsius) and assign its value to Property. 
// The property has a public getter and private setter. The property setter throws an exception if temperature is less than 273.15 Celsius. 
// Then, create a property Fahrenheit that will convert Celsius to Fahrenheit (it has no setter similar to NicePrintout) Bonus: create Kelvin property

public class Temperature
{
  private decimal _celsius;
  public decimal Celsius
  {
    get => _celsius;
    private set
    {
      if (value < -273.15m)
      {
        throw new Exception("Och , temperatuer is less than -273.15 ");
      }
      _celsius = value;
    }
  }
  public Temperature(decimal celcius)
  {
    Celsius = celcius;
  }

  public decimal GetFahrenheit() => (Celsius * (decimal)1.8) + 32;
  public decimal GetCentigrade() => (Celsius - 32) * (decimal)0.5556;

}
