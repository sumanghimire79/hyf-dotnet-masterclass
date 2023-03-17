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

/*3.
A POST endpoint that takes a lists of ids and retrieves all
of the users with those ids from the GET users (Id, FirstName, LastName and Age)
*/

// {
//     "id":[9,2,3]
// }
// /users/listOfUsers
app.MapPost("/users/listOfUsers", async (IDClass IDarray) =>
{
  var result = await GetListOfUsers(IDarray);
  return Results.Ok(result);
});

/*4.
A POST endpoint that takes a lists of ids and retrieves all of the products with those ids GET products(Id, Title)

*/
// {
//     "id":[99,2,3,7,9]
// }
// /users/listOfProducts
app.MapPost("/users/listOfProducts", async (IDClass IDarray) =>
{
  var result = await GetlistOfProducts(IDarray);
  return Results.Ok(result);
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
async Task<object> PostUser(User dataBody)
{
  Console.WriteLine(dataBody.FirstName);
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PostAsJsonAsync("https://dummyjson.com/users/add", dataBody);
  var UserDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  var postResponce = new
  {
    StatusCode = postRequest.StatusCode,
    ReasonPhrase = postRequest.ReasonPhrase,
    data = UserDataResponce,
    Date = postRequest.Headers.Date,
  };
  return postResponce;
}

// 2.
async Task<Object> PostProduct(Product dataBody)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PostAsJsonAsync("https://dummyjson.com/products/add", dataBody);
  var UserDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  var postResponce = new
  {
    StatusCode = postRequest.StatusCode,
    ReasonPhrase = postRequest.ReasonPhrase,
    data = UserDataResponce,
    Date = postRequest.Headers.Date,
  };
  var product = dataBody.Title;
  return postResponce;
};

// 3.
async Task<object> GetListOfUsers(IDClass IDarray)
{
  var httpClient = new HttpClient();
  var responce = "";
  foreach (var id in IDarray.Id)
  {
    var postRequest = await httpClient.GetAsync($"https://dummyjson.com/users/{id}");
    var data = postRequest.Content.ReadFromJsonAsync<User>();
    responce += $" ID:{data.Result.ID}  Name:{data.Result.FirstName} {data.Result.LastName}  Age:{data.Result.Age}";
  }
  return responce;
};

// 4.
async Task<object> GetlistOfProducts(IDClass IDarray)
{
  var httpClient = new HttpClient();
  var responce = "";
  foreach (var id in IDarray.Id)
  {
    var postRequest = await httpClient.GetAsync($"https://dummyjson.com/products/{id}");
    var data = postRequest.Content.ReadFromJsonAsync<Product>();
    responce += $" Product:{data.Result.Title}  Price:{data.Result.Price}";
  }
  return responce;
};

// 5.
async Task<User> GetUsers(int id)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.GetAsync($"https://dummyjson.com/users/{id}");
  var UserDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  return UserDataResponce;
}

// 6.
async Task<Product> GetProducts(int id)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.GetAsync($"https://dummyjson.com/Products/{id}");
  var productDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  return productDataResponce;
}

// 7.
async Task<object> UpdateUser(User dataBody)
{
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PutAsJsonAsync($"https://dummyjson.com/users/{dataBody.ID}", dataBody);
  var userDataResponce = await postRequest.Content.ReadFromJsonAsync<User>();
  var postResponce = new
  {
    StatusCode = postRequest.StatusCode,
    ReasonPhrase = postRequest.ReasonPhrase,
    data = userDataResponce,
    Date = postRequest.Headers.Date,
  };
  return postResponce;
}

// 8.
async Task<Object> UpdateProduct(Product dataBody)
{
  Console.WriteLine(dataBody.ID);
  var httpClient = new HttpClient();
  var postRequest = await httpClient.PutAsJsonAsync($"https://dummyjson.com/products/{dataBody.ID}", dataBody);
  var ProductDataResponce = await postRequest.Content.ReadFromJsonAsync<Product>();
  var postResponce = new
  {
    StatusCode = postRequest.StatusCode,
    ReasonPhrase = postRequest.ReasonPhrase,
    data = ProductDataResponce,
    Date = postRequest.Headers.Date,
  };
  return postResponce;
}

// 9.
async Task<Object> deleteUser(int id)
{
  var httpClient = new HttpClient();
  var deleteRequest = await httpClient.DeleteAsync($"https://dummyjson.com/users/{id}");
  var deleteResponce = await deleteRequest.Content.ReadFromJsonAsync<User>();
  var result = new
  {
    StatusCode = deleteRequest.StatusCode,
    ReasonPhrase = deleteRequest.ReasonPhrase,
    data = deleteResponce,
    Date = deleteRequest.Headers.Date,
  };
  return result;
}

// 10.
async Task<Object> DeleteProduct(int id)
{
  var httpClient = new HttpClient();
  var deleteRequest = await httpClient.DeleteAsync($"https://dummyjson.com/products/{id}");
  var deleteResponce = await deleteRequest.Content.ReadFromJsonAsync<Product>();
  var result = new
  {
    StatusCode = deleteRequest.StatusCode,
    ReasonPhrase = deleteRequest.ReasonPhrase,
    data = deleteResponce,
    Date = deleteRequest.Headers.Date,
  };
  return result;
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

// record IDClass(int[] Id);
public class IDClass
{
  public int[] Id { get; set; }
  public IDClass(int[] id)
  {
    Id = id;
  }
}
