var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// Exercise 1: String manipulation
// Complete the solution so that it reverses the string passed into it.

app.MapGet("/", () =>
{
  string input = "world";
  string reversed = ReverseString(input); //TODO: Implement ReverseString

  string ReverseString(string inputText)
  {

    char[] splittedInputText = inputText.ToCharArray();
    Array.Reverse(splittedInputText);

    return new string(splittedInputText);
  }

  Console.WriteLine($"Reversed input value: {reversed}");
  return ($"Reversed input value: {reversed}");

});

app.Run();
