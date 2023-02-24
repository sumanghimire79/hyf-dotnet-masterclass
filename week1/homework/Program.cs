var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/info", () => $"hello " + Environment.Version);


// Question 1: string manipuation
app.MapGet("/1", () =>
{
  string input = "suman ghimire";

  string reversed = ReverseString(input); //TODO: Implement ReverseString

  string ReverseString(string word)
  {
    char[] array = word.ToCharArray();
    Array.Reverse(array);
    return new String(array);
  }

  Console.WriteLine($"Reversed input value: {reversed}");
  return ($"Reversed input value: {reversed}");
});

app.Run();
