var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// Caclulator
// Make a GET endpoint that will take parameter number1 and number2 and based on operation 
// parameter will perform one of following operations: addition, substraction, multiplication. 
// If number1 or number2 are not a number return bad request response. For operation valid values 
// are add, substract, multiplay.
app.MapGet("/1", (string number1, string number2, string op) =>
{
  var isNumber1Valid = int.TryParse(number1, out var parsedNumberone);
  var isNumber2Valid = int.TryParse(number2, out var parsedNumbertwo);

  if (!isNumber1Valid || !isNumber2Valid) return Results.BadRequest($"inputs must be number");

  int result = 0;
  switch (op)
  {
    case "add":
      result = parsedNumberone + parsedNumbertwo;
      break;
    case "substract":
      result = parsedNumberone - parsedNumbertwo;
      break;
    case "multiply":
      result = parsedNumberone * parsedNumbertwo;
      break;
    default:
      return Results.BadRequest($"your operator is not valid");
  }
  return Results.Ok(result);
});

// 2. Method example
// Make a GET endpoint that will take input parameter. If input parameter is an integer call AddNumbers method 
// that receives input and returns sum of all integer values. If input is not an integer then call method CountCapitalLetters
// method that receives input and returns counter of all caputal letters.
// Hint: use int.TryParse() and char.IsUpper()
// Example 1: GET /?input=153 would calculate 1 + 5 + 3 and return 9. Example 2: GET /?input=The Quick Brown Fox Jumps Over the Lazy Dog will return 8.

app.MapGet("/2", (string input) =>

{
  var isNumber1Valid = int.TryParse(input, out var parsedInput);

  if (isNumber1Valid)
  {
    int AddNumbers(int parsedInput)
    {
      var sum = 0;
      while (parsedInput > 0)
      {
        sum = sum + parsedInput % 10;
        parsedInput = parsedInput / 10;
      }
      return sum;
    }
    return Results.Ok(AddNumbers(parsedInput));
  }
  int CountCapitalLetters(string input)
  {
    int count = 0;
    foreach (var c in input)
    {
      if (char.IsUpper(c)) count++;
    }
    return count;
  }
  return Results.Ok(CountCapitalLetters(input));
});

// 3. Distinct alphabetical list
// Make a GET endpoint that takes a string as input and returns 
// a new list containing only the unique characters, sorted in alphabetical order.
//  For example, if the input string is The cool breeze whispered through the trees, 
// the output should be ["b", "c", "d", "e", "h", "i", "l", "o", "p", "r", "s", "t", "u", "w", "z"].

app.MapGet("/3", (string input) =>
{
  var result = new List<char>();
  List<char> testList = new List<char>();
  foreach (char c in input)
  {
    if (char.IsLetter(c))
    {
      result.Add(c);
    }
    testList = result.Distinct().ToList();
    testList.Sort();
  }
  return Results.Ok(testList);
});


app.Run();
