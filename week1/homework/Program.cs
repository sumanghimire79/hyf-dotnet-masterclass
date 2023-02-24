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

//2. String/Math
// Return the number (count) of vowels in the given string. Consider a, e, i, o, u as vowels.
app.MapGet("/2", () =>
{
  string input = "IntellectualizationOOYa";
  int vowelCount = GetVowelCount(input.ToLower()); //TODO: Implement GetVowelCount

  int GetVowelCount(string word)
  {
    int count = 0;
    foreach (char c in word)
    {
      if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
      {
        count += 1;
      }
    }
    return count;
  }
  Console.WriteLine($"Number of vowels: {vowelCount}");
  return ($"Number of vowels: {vowelCount}");
});


app.Run();
