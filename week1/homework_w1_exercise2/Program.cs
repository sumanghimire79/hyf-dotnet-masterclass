var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//String/Math
// Return the number (count) of vowels in the given string. Consider a, e, i, o, u as vowels.

app.MapGet("/", () =>
{
  string input = "IntellectualizationUU";
  int vowelCount = GetVowelCount(input.ToLower()); //TODO: Implement GetVowelCount
  int GetVowelCount(string word)
  {
    int count = 0;
    foreach (Char letter in word)
    {
      if (letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u')
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
