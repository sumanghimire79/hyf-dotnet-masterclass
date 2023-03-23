var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// 1. Word Frequency Count
// Write a GET endpoint that takes a string as input and counts the frequency of each word in the string. 
// Your program should output a list of objects where each object contains a word and its frequency. 
// For example, if the input string is "the quick brown fox jumps over the lazy dog", your program should 
// output the following list: [("the", 2), ("quick", 1), ("brown", 1), ("fox", 1), ("jumps", 1), ("over", 1), ("lazy", 1), ("dog", 1)]

app.MapGet("/1", (string input) =>
{
  var wordsArray = input.Trim().Split(" ");
  Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(wordsArray));
  Dictionary<string, int> repeatedWordCount = new Dictionary<string, int>();
  foreach (string word in wordsArray)
  {
    if (repeatedWordCount.ContainsKey(word))
    {
      int count = repeatedWordCount[word];
      repeatedWordCount[word] = count + 1;
    }
    else { repeatedWordCount.Add(word, 1); }
  }
  return repeatedWordCount;
});

app.Run();
