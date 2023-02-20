# Examples

## Arrays

```csharp
int[] oneDimension = new int[10]; // 10 elements
// int[,] twoDimensions = new int[10,10]; // 100 
// int[,,] threeDimensions = new int[10,10,10]; // 1000...
// int[,,,,,,,,,] tenDimensions = new int[10,10,10,10,10,10,10,10,10,10]; // 10000000000...
// int[][] jagged = new int[10][];


var list = new List<int>();
```

## Simple properties

```csharp
var pt1 = new ScreenPoint();

Console.WriteLine($"({pt1.X},{pt1.Y})");

public class ScreenPoint
{
    private int _x = 0, _y = 0;

    // public ScreenPoint()
    // {
    //     Console.WriteLine("Aha! A new one!");
    // }

    // public ScreenPoint(int x)
    // {
    //     _x = x;
    // }

    public ScreenPoint(int x = 0, int y = 0)
    {
        _x = x;
        _y = y;
    }

    public int X
    {
        get
        {
            return _x;
        }
        set
        {
            if (value < 0) _x = 0;
            else if (value > 640) _x = 640;
            else _x = value;
        }
    }

    public int Y
    {
        get { return _y; }
        set
        {
            if (value < 0) _y = 0;
            else if (value > 480) _y = 480;
            else _y = value;
        }
    }
}
```

## Class

```csharp
var person = new Person()
{
    Name = "asdasd"
};

Console.WriteLine($"The name of the person is '{person.Name}'");


public class Person
{
    private string _name;

    public string Name
    {
        get { return _name; }
        set
        {
            if (value == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("What? A person that is null???? You are a dissapointment to humanity");

                throw new Exception("Ouch, name is null");
            }

            _name = value.Trim();
        }
    }
}
```

## Screen point

```csharp
var pt = new ScreenPoint();
pt.X = 600;
pt.Y = 600;

var pt2 = new ScreenPoint()
{
    X = 100,
    Y = 100
};

// print out
Console.WriteLine($"{pt.X} {pt.Y}");

public class ScreenPoint
{
    private int _x = 0, _y = 0;

    public int X
    {
        get
        {
            return _x;
        }
        set
        {
            if (value < 0) _x = 0;
            else if (value > 640) _x = 640;
            else _x = value;
        }
    }

    public int Y
    {
        get { return _y; }
        set
        {
            if (value < 0) _y = 0;
            else if (value > 480) _y = 480;
            else _y = value;
        }
    }
}
```

## Counter

```csharp
var counter = new Counter();
var counter2 = new Counter();

RepeatNTimes(counter, 10);

Console.WriteLine(counter.Count);
Console.WriteLine(counter2.Count);

void RepeatNTimes(Counter cnt, int howMany)
{
    for (var i = 0; i < howMany; i++)
        cnt.Inc();
}

class Counter
{
    public int Count { get; private set; }

    public void Inc()
    {
        Count++;
    }

    public void Reset()
    {
        Count = 0;
    }
}
```

## Meals

```csharp
// 1. Create Meal class that contains name, description, price, number of people, type (Indian, Chinese, Italian, Japanese)
// kinda use it?

try
{
    var meal = new Meal("Pasta", 100);
    var meal2 = new Meal("Schwarma", -100);

    Console.WriteLine($"I got a meals to eat: {meal.Name} and {meal2.Name}");
}
catch (Exception ex)
{
    Console.WriteLine("Uh-oh! Something wrong! I wonder: " + ex.Message);
}

class Meal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int NumberOfPeople { get; set; }
    public string Type { get; set; }

    public Meal(string name, decimal price, string type = "unknown")
    {
        if (name == null) throw new Exception("NAME SHOULD BE HERE!!!");
        if (price < 0) throw new Exception("Am I being paid to eat???");

        Name = name;
        Price = price;
        NumberOfPeople = 1;
        Type = type;
    }
}
```


## Inheritance

```csharp










Console.WriteLine("");

var dog = new Dog { Name = "Spotty" };
var cat = new Cat { IsInsane = true, Name = "Garfield" };

var animals = new List<Animal>();
animals.Add(dog);
animals.Add(cat);

foreach (var animal in animals)
{
    Console.WriteLine($"I found: {animal.Name}");
}

class Animal
{
    public string Name { get; set; }
}

class Dog : Animal
{
    public bool IsOnALeash { get; set; }
}

class Cat : Animal
{
    public bool IsInsane { get; set; }
}
```
