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

//3. Math/Array
//Given an array of the numbers return an array with two elements where first element represents sum of all negative numbers and second element represents multiplication of all positive numbers;

app.MapGet("/3", () =>
{
  int[] arr = new[] { 271, -3, 1, 14, -100, 13, 2, 1, -8, -59, -1852, 41, 5 };
  int[] result = GetResult(arr); //TODO: Implement GetResult
  int[] GetResult(int[] array)
  {
    int countP = 0;
    int countN = 1;

    foreach (int item in arr)
    {
      if (item < 0) countP += item;
      if (item > 0) countN *= item;
    }
    return new[] { countP, countN };
  }

  Console.WriteLine($"Sum of negative numbers: {result[0]}. Multiplication of positive numbers: {result[1]}");
  return ($"Sum of negative numbers: {result[0]}. Multiplication of positive numbers: {result[1]}");
});


//5. Arrays
//Given an integer array as an input, if the length of the array is not even write the warning message, otherwise split the array in half and add both resulting arrays together and write the result.
app.MapGet("/5", () =>
{
  int[] input = new[] { 1, 2, 5, 7, 2, 3, 5, 7 };
  int mid = input.Length / 2;
  int[] first = input.Take(mid).ToArray();
  int[] second = input.Skip(mid).ToArray();
  var sumArr = new int[mid];


  if (input.Length % 2 != 0)
  {
    Console.WriteLine($"{input} array must have even length");
  }
  else
  {

    for (var i = 0; i < first.Length; i++)
    {
      var sum = 0;
      sum = first[i] + second[i];
      sumArr[i] = sum;
    }
  }
  Console.WriteLine($"{sumArr}");
  return sumArr;
});




app.Run();
