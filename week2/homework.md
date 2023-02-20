# Homework

## How to deliver homework

Open this template repository https://github.com/HackYourFuture-CPH/dotnet-masterclass and click on ![image](https://user-images.githubusercontent.com/6642037/115988976-3796da80-a5bc-11eb-9184-554a2218b2ae.png) and then create a copy of this structure on your own GitHub profile with the name `hyf-dotnet-masterclass`

Create a PR to add your homework to the respective week folder like you are used to do in the web development course, and if you don't remember how to do hand in homework using Pull Requests, please check here https://github.com/HackYourFuture-CPH/JavaScript/blob/master/javascript1/week1/homework.md

## Homework exercises for Week #2 - Jupiter and time

Our mission to Mars has failed and we accidentally landed on Jupiter! Something is wrong - day here lasts only 10 hours! We need to build a timekeeping system.

### 1. A simple tracker

Let's create a new class called `JupiterTime`. It needs to have two properties: `Hours` and `Minutes` so that the following code works:

```cs
var time = new JupiterTime();
time.Hours = 8;
time.Minutes = 40;
```

Now create a function called `PrintTime` which accepts `JupiterTime` as a parameter and prints out `HH:mm`. If we run:

```cs
var time = new JupiterTime();
time.Hours = 8;
time.Minutes = 40;

PrintTime(time);
```

We should get `8:40` as an output. Think what happens when minutes are less than 10.

### 2. Adding constructor

Let's add constructor so this becomes possible:

```cs
var time = new JupiterTime(7, 40);
```

Ooops, some of our crew members accidentally wrote:

```cs
var time = new JupiterTime(14, 88);
```

This should actually become equal to `5:28`. Add the code to fix overflow.

### 3. Adding time

We need to add method `AddHours` which accepts a number which returns a new time object with the added hours. Let's look at an example:

```cs
var time = new JupiterTime(2, 20);
var timeIn1Hour = time.AddHours(1);

PrintTime(timeIn1Hour);
```

The code should print `3:20`.

> Bonus: What happens if we add a large number like `11`?
> Bonus: What happens if we add a negative number? How do we go back? What is before `0:00` for example?

### 4. Adding minutes

Just like above, we need a new method that adds minutes called `AddMinutes`. Let's look at an example code:

```cs
var time = new JupiterTime(1, 21);
var timeIn20Minutes = time.AddMinutes(20);

PrintTime(timeIn20Minutes);
```

It should print out `1:41`.

> Bonus: What happens if we add a large number like `80`?
> Bonus: What happens if we add a negative number? How do we go back? What is before `2:00` for example?

### 5. Better printout

We don't actually like `PrintTime`, it would be better if `Console.WriteLine(time)` just works. Turns out it does work, but we need to help it a bit.

Check out https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-override-the-tostring-method and try it out yourself.

### 6. Let's get things done

Turns out we need to send signals at designated times to Earth. But keeping time is really complicated. Let's build a tool for reminding us when to send signals.

To do this, let's create a new class called `Signaler`. It will keep a list of all times when we need to send a signal to Earth. To do that, it has a method `AddTimer` so that the following code works:

```cs
var signaler = new Signaler();
signaler.AddTime(new JupiterTime(1, 20));
signaler.AddTime(new JupiterTime(2, 20));
signaler.AddTime(new JupiterTime(3, 20));
```

When we call method `Inform` it needs to print out a list of times that were added so far. If nothing was added, it should print `No timers added yet`.

### 7. We fell asleep! What should we do now?

As we woke up we realized that we probably forgot to send signals. Create a method `Check` which accepts a time (right now) and checks how many signals we forgot to send.

For example:

```cs
var signaler = new Signaler();
signaler.AddTime(new JupiterTime(2, 00));
signaler.AddTime(new JupiterTime(4, 00));
signaler.AddTime(new JupiterTime(6, 00));

// We woke up at 4:21
signaler.Check(new JupiterTime(4, 21));
```

The method should print out a list of signal times we forgot to send. In the above example it should print out:

```
2:00
4:00
```

If we called it with `signaler.Check(new JupiterTime(6, 21));` it should print all three added timers.
If we called it with `signaler.Check(new JupiterTime(1, 17));` it should print `No signals needed to be sent yet`.

### 8. We flew over to Titan

...only to realize that Titan has day that lasts 15.9 Earth days which means its day lasts 900 hours! Ouch. We need to make _another_ class called `TitanTime` which works just like `JupiterTime`, but it works slightly different.

1. `new TitanTime(1000, 40)` should be the same as `new TitanTime(100, 40)`. We discard extra hours.
2. `new TitanTime(30, 70)` should be the same as `new TitanTime(31, 10)`. We turn extra minutes into hours.
3. Printing time should be in format `000:00` i.e., with leading zeroes. So that `new TitanTime(0, 0)` gets printed as `000:00`, `new TitanTime(9, 11)` is `009:11`, `new TitanTime(11, 7)` is `011:07`.

Since our mission won't last long, can you quickly write a similar class?

### 9. Ganymede calls

Now back to Jupiter! This time another change in time...one day lasts 7 Earth days and 3 hours. We can copy past and fix our class _again_, but surely there is a better way? We remember what we've learned and we start applying our knowledge of OOP. Let's create an abstract class called `AlienTime`:

```cs
public abstract class AlienTime
{}
```

We need to have the following:

- A property `Hours` which returns current hours
- A property `Minutes` which returns current minutes
- A constructor that accepts 3 parameters: `hours`, `minutes`, `hoursInDay`. The last one will be stored in a private field.
- Override `ToString` to print out the time nicely.

With this in place we can write the following classes:

```cs
public class JupiterTime : AlienTime
{
    public TitanTime(int hours, int minutes)
        : base(hours, minutes, 10)
    {}
}

public class TitanTime : AlienTime
{
    public TitanTime(int hours, int minutes)
        : base(hours, minutes, 900)
    {}
}

public class GanymedeTime : AlienTime
{
    public TitanTime(int hours, int minutes)
        : base(hours, minutes, 171)
    {}
}
```

### 10. Extra comment - generics (bonus - information purpose only)

Can we add `AddHours` to `AlienTime`? On the first glance, we cannot since we don't know what the return type should be. The return type should be the one _inheriting_.

```cs
JupiterTime time = new JupiterTime().AddHours(1);
TitanTime time = new TitanTime().AddHours(1);
```

If `AddHours` is in `AlienTime`, how can it possibly know to switch types? The answer is generics. And a helper method for creating a new instance. Let's assume we start with the following implementation:

```cs
public abstract class AlienTime
{
    public int Hours { get; set; }
    public int Minutes { get; set; }

    /// What type should be here?
    public ?? AddHours(int hours)
}
```

First we modify our class to be `AlienTime<T>` where `<T>` means _a type called T_. This means that right now we have no idea what that type will be, but whoever is using the class will know and they will tell us. It is like parameter, but instead of number, string or an object, this is a type. And it allows us to create a _hole_ we can fill later. The class should look like this:

```cs
public abstract class AlienTime<T>
{
    public int Hours { get; set; }
    public int Minutes { get; set; }

    /// What type should be here?
    public T AddHours(int hours)
    {
        // how do we construct this now?
    }
}
```

Hmmm, we still don't know how to create this ... `T` or whatever it is. Let's do a small trick and create a method that will do just that:

```cs
public abstract class AlienTime<T>
{
    public int Hours { get; set; }
    public int Minutes { get; set; }

    /// What type should be here?
    public T AddHours(int hours)
    {
        return Create(Hours + hours, minutes);
    }

    protected abstract T Create(int hours, int minutes);
}
```

Alright, this now compiles. But how do we use it? Since we want to reuse this base class, we can implement `JupiterTime` on top of this:

```cs
public class JupiterTime : AlienTime<JupiterTime>
{
    protected override JupiterTime Create(int hours, int minutes)
    {
        return new JupiterTime { Hours = hours, Minutes = minutes };
    }
}
```

And now the following code works as expected:

```cs
var now = new JupiterTime(1, 31);

// correct type!
JupiterTime later = now.AddHours(1);
```

This ends our incursion into generics. Feel free to experiment with this code, look for more examples or ask for more details.
