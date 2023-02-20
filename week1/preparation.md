# Week 1 - Preparation

Please follow [getting started](https://github.com/HackYourFuture-CPH/dotnet-masterclass/blob/main/getting-started.md) guide first.

## Introduction

### What is .NET?

.NET is a cross platform, free, and open-source [software framework](https://en.wikipedia.org/wiki/Software_framework) for building applications and libraries developed by Microsoft. During this masterclass we will use .NET 7, the latest version of .NET. You might read about .NET Framework (Windows only) or .NET Core (versions 1 through 3.1), but that is not part of the lesson. If you want to read more about it, read the official docsL [.NET vs. .NET Framework](https://learn.microsoft.com/en-us/dotnet/standard/choosing-core-framework-server).

It is usually said that .NET is [managed](https://learn.microsoft.com/en-us/dotnet/standard/managed-code). Simply put, the underlying .NET runtime manages the code you write - it compiles your code into machine code, provides automated memory management, security and type safety (we will talk about some of this later).

#### **Compiled into machine code? What does that mean?**

Compilation refers to translation of human readable source code into code that the processor can run. To put it very simply, the code you write will be translated into a format that is suitable for running on a processor. The end result of a compilation step is a file - either an `.exe` or a `.dll` file.

#### **How is that different from JavaScript?**

When you run JavaScript, the engine will use the source code without translation. This is sometimes referred to as _interpreted_ execution. An excellent example of this is the nodejs project (the below example is from [Week 1 HYF nodejs](https://github.com/HackYourFuture-CPH/node.js/blob/main/week1/exercises/01-server.md#server)). .NET requires a step to compile your code before running it.

```jsx
$ node app.js
```

This command will instruct the node.js to run app.js directly without “building” it.

#### **But I heard that you could build JavaScript code.**

Usually, when someone says that it is “building” JavaScript code, this happens when someone is building or running a React project. This process is a combination of two steps: bundling and transpiling. The output of that operation is … another JavaScript file(s) which you can view and edit in your favorite text editor.

The bottom line is - building .NET project will give you an executable (assembly) that, if you try to open it with your favourite text editor, will look like a bunch of random characters. In the case of “building” JavaScript code, its output will still be a JavaScript code and it will probably resemble code that you wrote but is just a bit harder to read.

#### **So is it better to have an executable, then?**

It depends on how you look at it. To run the .NET application, there is an additional step - a **compilation (build)** that converts your code into machine code, and as your project grows, build time also grows. But having compiled version of your code gives you a bit of speed when the application is executing.

## What can we do with .NET?

Today, .NET is a [cross-platform](https://en.wikipedia.org/wiki/Cross-platform_software) [software framework](https://en.wikipedia.org/wiki/Software_framework). That means you can build things for macOS, Linux, Windows etc. And you can build all different kinds of applications:

- **Mobile** - using tools like [Xamarin](https://dotnet.microsoft.com/en-us/apps/xamarin) or [MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui), you can build mobile applications for iOS and Android with a single codebase!
- **Desktop** - you can quickly start building desktop applications with [Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/get-started/create-app-visual-studio), [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) or use one of many libraries like [Uno](https://platform.uno/) or [Avalonia UI](https://avaloniaui.net/) to build rich UI applications.
- **Web** - there are a lot of options here. We will be focusing on creating [Web API](https://dotnet.microsoft.com/en-us/apps/aspnet/apis) during this masterclass.
- **Games** - many libraries and frameworks are available for building games in .NET. Currently, one of the most popular ones out there is [Unity 3d](https://unity.com/).
- **Internet of Things (IoT)** - you can run .NET on devices like [Raspberry PI](https://dotnet.microsoft.com/en-us/apps/iot)
- etc.

## Languages

By default, you can write .NET in C#, F# and VB.NET. You can see all three languages with simple examples on [this page](https://dotnet.microsoft.com/en-us/apps/iot). There are some [other languages](https://en.wikipedia.org/wiki/List_of_CLI_languages) too, but they are much less popular. In this masterclass we will only be focused on C#.

## Tools

IDE of choice for the masterclass is VSCode but there are other options available out there too:

- [Microsoft Visual Studio](https://visualstudio.microsoft.com/)
- [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Microsoft Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)

You can use a plan text editor such as Vim, Sublime Text or any other, but the experience will vary and you will have to do extra steps to get the code to integrate nicely with the editor.

## Creating your first .NET Web API Application

After installing .NET SDK you are ready to create your first .NET application. And what better way to do that than creating a simple _hello world_ application?

Choose a folder where you will be keeping all .NET projects that we will be using during the course of .NET masterclass.

**Important**: if you make a mistake and find yourself in a hopeless situation, remove the folder (hello-world) and start again. This rule applies to all other exercises too.

For creating a hello world application, we will use `dotnet cli` (Command Line Interface). `dotnet cli` is used similarly to how you used npm/node during node.js course. You can use `dotnet cli` to create projects, run projects and add dependencies (libraries) to the project. There are many more commands available that you can discover [here](https://learn.microsoft.com/en-us/dotnet/core/tools/#basic-commands).

### **Creating a hello world project**

Creating and running `hello world` project by:

- Open the folder where you will be keeping all .NET projects and execute `mkdir hello-world` to create `hello-world` folder.
- `cd hello-world` - navigate to `hello-world` folder.
- `dotnet new web` - create a new project with `web` template.
- `dotnet run` command will run the current project. You will see a message like this one.

```powershell
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5008
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\dev\hyf-masterclass\hello-world
```

- In the browser, open the address that is displayed in the console (in my case, its [http://localhost:5008](http://localhost:5008) but you might have different one).
- You should be able to see `Hello, world!` message in the browser.
- Stop running the server by pressing `CTRL+C` in the console where server is running.

#### Doing it from VSCode

- You can open project in two ways:
  1. Open VSCode and select `Open Folder` from `File` menu (`ALT+F+O` or `CTRL+K+O` keyboard shortcut).
  2. Execute `code .` from the console.
- After opening the folder you will see a notification toaster in the lower right corner with the message: Required assets to build and debug are missing from ‘hello-world’. Add them?
  Click on **Yes.** That is **important.** If you missed it, reopen VSCode or open the command palette (`CTRL+SHIFT+P`) and type `Debug: Add Configuration` select it and choose `.NET  5+ and .NET Core`.
- You will notice that in explorer `.vscode` folder has been created.
- Run project by pressing the F5 command. If the command pallet pops up select `.NET  5+ and .NET Core` as the suggested option.
- You should see the same message about the server running and the address/port server is running on in the VSCode console. Also, the browser will probably automatically navigate you the default endpoint, where you will once again see `Hello, world!` message.
- Stop the server by clicking on the red square in the VSCode.
- Explore project. Open `Program.cs` try changing the text and running project with F5 again.

#### 🧑‍🏫 **Task:**

- Similar to the fashion of the node.js course, add `/info` route to `Program.cs`
- You can find the currently running version of the .NET from `Environment.Version`

## Before we continue

In the next following examples, we will be using some variable types that are covered in week 2.

- `int` is a built-in data type in C# that represents integer values. It can store whole numbers between -2,147,483,648 and 2,147,483,647. It is one of the most commonly used data types in C#, and is used to represent quantities, counts, and other numerical values.
  Common variable declaration is: `int number = 5;`
- `string` is another built-in data type in C# represent a sequence of characters. It is used to store text data, such as names, addresses, and other textual information. Strings are typically enclosed in double quotes ("") and can contain any combination of characters, including letters, numbers, and symbols.
  Common variable declaration is `string name = "Allan";`

Both `int` and `string` are fundamental data types in C#, and are used extensively. Understanding these data types is essential for working with C#.

## Controlling program flow (continue on top of the hello world)

Hello, world example is interesting but not so much exciting. Let us make it more interesting by throwing in a few `if/else` statements, looping over a block of code using `for`, `do while`, `while` or `foreach` and making `switch` statements. Sounds exciting?

**Branching with if/else statements**
You can write conditional `if/else` statements similar to how it is done in JavaScript. Read more about [if statement in .NET here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement).

A basic example of `if/else` statement is:

```csharp
if (condition)
{
	// ... do something here
}
```

`If/else` statements can become complicated with multiple else if statements

```csharp
if (condition1)
{
}
else if (condition2)
{
}
// other else if...
else
{
}
```

### **Example** (add it to hello-world project):

- Add a new route to `Program.cs`

```csharp
app.MapGet("/if-else", (int number) =>
{
    if (number >= 0)
    {
        return "Positive number";
    }
    else
    {
        return "Negative number";
    }
});


// this goes last
app.Run();
```

- Run the project by pressing `F5` and navigate to [http://localhost:5008](http://localhost:5008) (remember to change the port to the one you are running on)
- Navigate to [http://localhost:5008/if-else?number=1](http://localhost:5008/if-else?number=1) or [http://localhost:5008/if-else?number=-1](http://localhost:5008/if-else?number=-1)
- Alternatively, write `dotnet watch run` in console (in the root of your project)

> 🧑‍🏫 **Task:**
> Can you make this simpler using the ternary conditional operator?

### **Switching and pattern matching**

Same as `if/else`, Switch is also done similarly to how it is done in JavaScript but with some additions that came lately (pattern matching). You can [read more about switch in .NET here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement).
Usually, we use `switch` statement when we want to execute different block of code based on the value of a specified expression.

```csharp
switch (expression)
{
    case value1:
        // code block for case value1
        break;
    case value2:
        // code block for case value2
        break;
    ...
    default:
        // code block for default case (optional)
        break;
}
```

**Example**:

```csharp
app.MapGet("/greeting", (string name) =>
{
    string greeting;

    switch (name)
    {
        case "Kiran":
            greeting = "Hey, Kiran!";
            break;
        case "Zahra":
            greeting = "Hi, Zahra!";
            break;
        case "Divya":
            greeting = "Ola, Divya!";
            break;
        default:
            greeting = $"Hello, {name}!";
            break;
    }

    return greeting;
});

// this goes last
app.Run();
```

- Run the project by pressing `F5`
- Navigate to [http://localhost:5008/greeting?name=World](http://localhost:5008/greeting?name=World)

Pattern matching is a feature in the C# that has been recently added and does not exist in JavaScript (it’s currently in the [proposal phase](https://github.com/tc39/proposal-pattern-matching)). Pattern matching allows you to match the value of an expression against a set of patterns rather than just specific value(s). This makes writing more concise and expressive code, especially when dealing with complex data structures.

In a switch statement, pattern matching is used to specify the cases that should be executed based on the value of the expression. Each case specifies a pattern, and the matching pattern is executed if the expression matches the pattern.

```csharp
app.MapGet("/temperature", (int degrees) =>
{
    string description = "";
    switch (degrees)
    {
        case < 0:
            description = "freezing";
            break;
        case > 0 and < 10: // this is done just to show possibility < 10 is enough in this case but
            description = "cold";
            break;
        case < 25:
            description = "mild";
            break;
        default:
            description = "hot";
            break;
    };

    return $"The temperature of {degrees} degrees Celsius is considered {description}.";
});

// this goes last
app.Run();
```

### **Looping with for, for each, do and while**

- Add a new route to `Program.cs`

```csharp
app.MapGet("/for", (int number) =>
{
    int sum = 0;

    for (int i = 0; i <= number; i++)
    {
        sum += i;
    }

    return sum;
});
```

- Run the project by pressing `F5` and navigating to [http://localhost:5008](http://localhost:5008) (remember to change the port to the one you are running on).
- You should see 15 as a result.

Same can be done with a `do while` statement

```csharp
app.MapGet("/do-while", (int number) =>
{
    int sum = 0;
    int i = 0;

    do
    {
        sum += i;
        i++;
    } while (i <= number);

    return sum;
});

// this goes last
app.Run();
```

or with the `while` statement

```csharp
app.MapGet("/while", (int number) =>
{
    int sum = 0;
    int i = 0;

    while (i <= number)
    {
        sum += i;
        i++;
    }

    return sum;
});
```

`for each` statement is used for iterating over arrays/lists/enumerations

```csharp
app.MapGet("/for-each", () =>
{
    string myString = "Hello, World!";
    string result = "";

    foreach (char c in myString)
    {
        result += c + " ";
    }

    return result;
});
```

- Run the project by pressing `F5` and navigating to [http://localhost:5008](http://localhost:5008) (remember to change the port to the one you are running on).
- What do you see as a result?

## Conclusion

By this point you have managed to create a simple application using .NET and C#.
Feel free to experiment a bit more and try to alter examples to see what will happen.
Don't forget - curiosity and exploration is highly encouraged while learning a
new topic.

See you at the class!

## Additional links and materials

- [What is .NET? What is .NET? What's C#?](https://www.youtube.com/watch?v=bEfBfBQq7EE) (19 minutes)
- [Hello world in C#](https://www.youtube.com/watch?v=KT2VR7m19So&feature=youtu.be) (4:39 min) / Microsoft Learn link
- [Using .NET in Visual Studio Code](https://code.visualstudio.com/docs/languages/dotnet)
- [Create minimal api](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio-code#create-an-api-project)
