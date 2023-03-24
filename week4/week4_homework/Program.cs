var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "week4-homework");

/*1.
 A POST endpoint that calls POST users/add with a record
 with FirstName, LastName and Age (this simulates creating a user)
*/
// {
//     "firstName":"Suman",
//     "lastName":"Ghimire",
//     "age":34
// }
///users/add
app.MapPost("/users/add", async (User dataBody) =>
{
  var result = await PostUser(dataBody);
  return Results.Ok(result);
});

/*2.
A POST endpoint that that calls Post products/add 
with a record with Title and Price (this simulates creating a product)
*/
// {
//     "title":"my product",
//     "price": 20
// }
// /products/add
app.MapPost("/products/add", async (Product dataBody) =>
{
  var result = await PostProduct(dataBody);
  return Results.Ok(result);
});

/*
 3.A POST endpoint that takes a lists of ids and retrieves all
of the users with those ids from the GET users (Id, FirstName, LastName and Age)
 4.A POST endpoint that takes a lists of ids and retrieves all of the products with those ids GET products(Id, Title)
*/
app.MapPost("/postNget", async (RequestInList request) =>
{
  if (request.UsersORproducts == "users")
    return Results.Ok(await GetListOfItems<User>(request));
  if (request.UsersORproducts == "products")
    return Results.Ok(await GetListOfItems<Product>(request));
  return Results.BadRequest(" Request is not supported currently !");
});

/*
OPTIONAL:
5. A GET endpoint that gets a user based on an id
6. A GET endpoint that gets a product based on an id
7. A PUT endpoint that updates a user based on an id and the body of the request
8. A PUT endpoint that updates a product based on an id and the body of the request
9. A DELETE endpoint that deletes a user based on an id
10. A DELETE endpoint that deletes a product based on an id
*/

/* 5. A GET endpoint that gets a user based on an id*/
// /getUsers?id=1
app.MapGet("/getUsers", async (int id) =>
{
  var result = await GetUsers(id);
  return Results.Ok(result); ;
});

/*6.  A GET endpoint that gets a product based on an id*/
// /getProducts?id=1
app.MapGet("/getProducts", async (int id) =>
{
  var result = await GetProducts(id);
  return Results.Ok(result); ;
});

/*7. A PUT endpoint that updates a user based on an id and the body of the request*/
// {
//     "FirstName":"amrit",
//     "LastName":"Ghimire",
//     "Age":34,
//     "id":1
// }
// /updateUser
app.MapPut("/updateUser", async (User dataBody) =>
{
  Console.WriteLine(dataBody.ID);
  var result = await UpdateUser(dataBody);
  return Results.Ok(result); ;
});

/* 8. A PUT endpoint that updates a product based on an id and the body of the request */
// {
//     "title":"my /sproduct",
//     "price": 20.20,
//     "id":33
// }
// /updateProduct
app.MapPut("/updateProduct", async (Product dataBody) =>
{
  Console.WriteLine(dataBody.ID);
  var result = await UpdateProduct(dataBody);
  return Results.Ok(result); ;
});

/* 9. A DELETE endpoint that deletes a user based on an id */
// /deleteUser?id=1
app.MapDelete("/deleteUser", async (int id) =>
{
  var result = await deleteUser(id);
  return Results.Ok(result); ;
});

/* 10. A DELETE endpoint that deletes a product based on an id */
// /deleteProduct?id=1
app.MapDelete("/deleteProduct", async (int id) =>
{
  var result = await DeleteProduct(id);
  return Results.Ok(result); ;
});

app.Run();

// 1.
async Task<ReturnObj<User>> PostUser(User dataBody)
{
  Console.WriteLine(dataBody.FirstName);
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PostAsJsonAsync("https://dummyjson.com/users/add", dataBody);
  var UserDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  ReturnObj<User> retobj = new ReturnObj<User>(postRequest.StatusCode, postRequest.ReasonPhrase, UserDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return retobj;

}

// 2.
async Task<ReturnObj<Product>> PostProduct(Product dataBody)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PostAsJsonAsync("https://dummyjson.com/products/add", dataBody);
  var ProductDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  ReturnObj<Product> returnObj = new ReturnObj<Product>(postRequest.StatusCode, postRequest.ReasonPhrase, ProductDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return returnObj;
};

// 3.//4.
async Task<List<T>> GetListOfItems<T>(RequestInList request)
{
  var listOfTaskItems = new List<Task<T>>();
  foreach (var id in request.ID)
  {
    var retrievedItem = GetItem<T>(request.UsersORproducts, id);
    listOfTaskItems.Add(retrievedItem);
  }
  return (await Task.WhenAll(listOfTaskItems)).ToList();
}
async Task<T> GetItem<T>(string endpoint, int id)
{
  var request = await new HttpClient().GetAsync($"https://dummyjson.com/{endpoint}/{id}");
  var listOfUsersORproducts = await request.Content.ReadFromJsonAsync<T>();
  return listOfUsersORproducts;
}

// 5.
async Task<ReturnObj<User>> GetUsers(int id)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.GetAsync($"https://dummyjson.com/users/{id}");
  var UserDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  ReturnObj<User> retobj = new ReturnObj<User>(postRequest.StatusCode, postRequest.ReasonPhrase, UserDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return retobj;
}

// 6.
async Task<ReturnObj<Product>> GetProducts(int id)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.GetAsync($"https://dummyjson.com/Products/{id}");
  var productDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  ReturnObj<Product> returnObj = new ReturnObj<Product>(postRequest.StatusCode, postRequest.ReasonPhrase, productDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return returnObj;
}

// 7.
async Task<ReturnObj<User>> UpdateUser(User dataBody)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PutAsJsonAsync($"https://dummyjson.com/users/{dataBody.ID}", dataBody);
  var userDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  ReturnObj<User> retobj = new ReturnObj<User>(postRequest.StatusCode, postRequest.ReasonPhrase, userDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return retobj;
}

// 8.
async Task<ReturnObj<Product>> UpdateProduct(Product dataBody)
{
  Console.WriteLine(dataBody.ID);
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PutAsJsonAsync($"https://dummyjson.com/products/{dataBody.ID}", dataBody);
  var ProductDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  ReturnObj<Product> returnObj = new ReturnObj<Product>(postRequest.StatusCode, postRequest.ReasonPhrase, ProductDataResponce, (System.DateTimeOffset)postRequest.Headers.Date);
  return returnObj;
}

// 9.
async Task<ReturnObj<User>> deleteUser(int id)
{
  var httpClient = new HttpClient();
  var deleteRequest = await httpClient.DeleteAsync($"https://dummyjson.com/users/{id}");
  var deleteResponce = await deleteRequest.Content.ReadFromJsonAsync<User>();
  ReturnObj<User> retobj = new ReturnObj<User>(deleteRequest.StatusCode, deleteRequest.ReasonPhrase, deleteResponce, (System.DateTimeOffset)deleteRequest.Headers.Date);
  return retobj;
}

// 10.
async Task<ReturnObj<Product>> DeleteProduct(int id)
{
  var httpClient = new HttpClient();
  var deleteRequest = await httpClient.DeleteAsync($"https://dummyjson.com/products/{id}");
  var deleteResponce = await deleteRequest.Content.ReadFromJsonAsync<Product>();
  ReturnObj<Product> returnObj = new ReturnObj<Product>(deleteRequest.StatusCode, deleteRequest.ReasonPhrase, deleteResponce, (System.DateTimeOffset)deleteRequest.Headers.Date);
  return returnObj;
}

// record User(string FirstName, string LastName, int Age);
class User
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int Age { get; set; }
  public int ID { get; set; }
  public User(string firstName, string lastName, int age, int id)
  {
    FirstName = firstName;
    LastName = lastName;
    Age = age;
    ID = id;
  }
}

// record Product(string Title, double Price);
class Product
{
  public string Title { get; set; }
  public double Price { get; set; }
  public int ID { get; set; }
  public Product(string title, double price, int id)
  {
    Title = title;
    Price = price;
    ID = id;
  }
}

// record RequestIDList(List<int> Ids);
public class RequestInList
{
  public string UsersORproducts { get; set; }
  public List<int> ID { get; set; }
  public RequestInList(List<int> id, string usersORproducts)
  {
    ID = id;
    UsersORproducts = usersORproducts;
  }
}

public class ReturnObj<T>
{
  public System.Net.HttpStatusCode StatusCode { get; set; }
  public string ReasonPhrase { get; set; }
  public T Data { get; set; }
  public System.DateTimeOffset CurrentDate { get; set; }
  public ReturnObj() { }
  public ReturnObj(System.Net.HttpStatusCode statuscode, string reasonfrase, T data, System.DateTimeOffset currentdate)
  {
    StatusCode = statuscode;
    ReasonPhrase = reasonfrase;
    Data = data;
    CurrentDate = currentdate;
  }
}

