using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
/*
< summary >
Endpoint for getgetting users. to get and return both user and host sametime 
when using record it needs empty construcure and when using class it needs both 
(empty and the other) constructure 

<example>
 implement a simple endpoint that fetches the current users in MySql.
  <questions>
  what does the code do? So let’s analyze what we just did:
  1.We created a new endpoint
  2.Inside we created a new connection to MySql
  3.We opened a connect
  4.Then we created a new command with SQL text inside
  5.We executed the command
  6.In a loop we read rows and mapped columns to our model
  7.Return the list to the caller

  Then after:Change the code to select both host and user from the database:
    1.Change the record for User and add string host
    2.Change SQL to select host
    3.Change the inner part of the loop to read user and host before creating a new object
  </questions>
</example>
</summary>
*/
app.MapGet("/users", async (IConfiguration configuration) =>
{
  using (var connection = new MySqlConnection(configuration.GetConnectionString("Default")))
  {
    connection.Open();
    using (var command = new MySqlCommand("SELECT * FROM mysql.user", connection))
    {
      using (var reader = await command.ExecuteReaderAsync())
      {
        var users = new List<User>();
        while (await reader.ReadAsync())
        {
          var user = reader.GetString(reader.GetOrdinal("user"));
          var host = reader.GetString(reader.GetOrdinal("host"));
          users.Add(new User(user, host));
        }
        return Results.Ok(users);
      }
    }
  }
});

/*
< summary >
Endpoint for getgetting users. starting from C# 8 it no longer need inner {}
</summary>
*/
app.MapGet("/users2", async (IConfiguration configuration) =>
{
  // starting from C# 8 you no longer need inner {}
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var users = await connection.QueryAsync<User>("SELECT * FROM mysql.user");
  return Results.Ok(users);
});

/*
< summary >
Endpoint for getgetting users
</summary>
*/
app.MapGet("/products", async (IConfiguration configuration) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var products = await connection.QueryAsync<Product>("SELECT id, name, price,description FROM dapper.products");
  return Results.Ok(products);
});


/*
<question>
    Implement the endpoint /product/{id}that:
    1.Changes the SQL to add WHERE id = {id}
    2.Instead of a list, returns .FirstOrDefault
    3.Bonus: if there is no such user, the list would be empty. What should REST API return when you want a resource that doesn’t exist? E.g. you call it with /product/666?
    4.Bonus: if the user passes negative number e.g. -1, what should REST API return in that case?
</questions>
*/
app.MapGet("/products/{id}", async (IConfiguration configuration, int id) =>
{
  if (id < 0)
  {
    //#####
    throw new Exception("id can not be negative");//??
    // Results.BadRequest($" you entered id:{id},id can not be negative"); 
  }
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={id}");
  if (products.Count() == 0)
  {
    return Results.NotFound($" The  product with id:{id} does not exists in the database !!");
  }
  return Results.Ok(products);
});

/*Instead of a list, returns .FirstOrDefault*/
app.MapGet("/firstordefault/{id}", async (IConfiguration configuration, int id) =>
{
  if (id < 0)
  {
    throw new Exception("id can not be negative");
    // Results.BadRequest($" you entered id:{id},id can not be negative");
  }
  //#####
  // how to checko if the input is letter instead of number or is not a number???
  // var isNumeric = int.TryParse((string)id, out int value);
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={id}");
  if (products.Count() == 0)
  {
    throw new Exception("product id not found");
  }
  return products.FirstOrDefault();
});

/*
< summary >
Endpoint for getgetting users:
<questions>
  1.Implement validation to return 400 if the name is null or empty string.
  2.Bonus: Think what happens if you don’t send price in JSON?
</questions>
<example>
postman input
 <code>
 {
  "name": "pro name" ,
  "price": 66.99 
 }
</code>
</example>
</summary>
*/
app.MapPost("/products", async (IConfiguration configuration, Product product) =>
{
  // it also works #####??
  // if (product.Name == "" || product.Name == null || product.Price == null) // how to check null or empty for decimal???
  // {
  //   return Results.BadRequest($" name: '{product.Name}' or price: '{product.Price}' is not valid, please provide valid data !!");//400 badrequest
  // }
  if (string.IsNullOrEmpty(product.Name))
  {
    return Results.BadRequest($" name: '{product.Name}' or price: '{product.Price}' is not valid, please provide valid data !!");
  }
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var producPost = await connection.ExecuteAsync("INSERT INTO dapper.products (name, price, description) VALUES (@name, @price, @description)", product);// how to take the id that is recently posted??
  return Results.Created($"/product/{producPost}", product); //  201 created
});

/*
< summary >
Endpoint for getgetting users:
<questions>
  1.Updating a product is done by handling a PUT endpoint on a /product/{id} endpoint.
  2.What do you return from a PUT endpoint?
</questions>
<example>
postman input
 <code>
 {
  "name": "p name " ,
  "price": 66.99 
 }
</code>
</example>
</summary>
*/
app.MapPut("/products/{id}", async (IConfiguration configuration, Product product, int id) =>
{
  if (product.Name == "" || product.Name == null || product.Name == " " || product.Price == null) // how to checko null or empty for decimal???
  {
    return Results.BadRequest($" '{product.Name}' or '{product.Price}' is not valid, please provide valid data !!"); //400 bad request
  }
  // it only updates the product available but it doesnot set the id to that id that already not exists.. ???
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var productUpdated = await connection.ExecuteAsync($"UPDATE dapper.products SET name=@name, price=@price,description=@description WHERE id={id}", product);
  // var productId = await connection.ExecuteAsync($"UPDATE dapper.products SET name={product.Name}, price={product.Price} WHERE id={product.Id}", product);// ##### seems logical, but why this does not work?? 
  if (productUpdated == 0)
  {
    return Results.NotFound($"this id: {id} not found");//404 not found
  }
  //##### id is set to 0 ?? doesnot work??
  return Results.Accepted($"/product/{id}", product); // 202 Accepted
});


/*
<questions>
  1.Implement an endpoint /product/{id} that takes that id and tries to delete the product with that ID
  2.Think about what should the endpoint return.
  3.What happens if there is no such product? What is the return of that endpoint?
</qustions>
*/
app.MapDelete("/products/{id}", async (IConfiguration configuration, int id) =>
{
  if (id < 0)
  {
    return Results.BadRequest("id does not exists");//400 badrequest
  }
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var deletedProduct = await connection.ExecuteAsync("DELETE FROM dapper.products WHERE id=@id", new { ID = id });//#####
  if (deletedProduct == 0)
  {
    return Results.NotFound($" this product id:{id} not found");//404 not found
  }
  return Results.Ok($"{id} deleted");//200 ok
});


// join table ?? #####

// SELECT *
// FROM ARTIST AS ART
// INNER JOIN ALBUM AS ALB
// ON ART.ARTIST_ID = ALB.ARTIST_ID;
app.MapGet("/productsjoin", async (IConfiguration configuration) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  // var products = await connection.QueryAsync<Product>("SELECT dapper.products.id, name, price, description, dapper.sales.sales_date  FROM dapper.products join dapper.sales on dapper.products.id= dapper.sales.products_id ");
  // List<T> genericList = new List<T>();
  var products = await connection.QueryAsync<Product>("SELECT id, name, price, description FROM dapper.products");
  var sales = await connection.QueryAsync<Sales>("SELECT *  FROM dapper.sales ");
  // var salesman = await connection.QueryAsync<Salesman>("SELECT dapper.products.id, name, price, description, dapper.sales.sales_date  FROM dapper.products join dapper.sales on dapper.products.id= dapper.sales.products_id ");
  return Results.Ok(sales);
});


app.Run();

// public record User(string user, string host)
// { public User() : this("", "") { } }
public class User
{
  public string? user { get; set; } = "suman";
  public string? host { get; set; } = "ghimire";
  public User() { }// shows error without this empty constructor ??
  public User(string user, string host)
  {
    this.user = user;
    this.host = host;
  }
}

record Product(int Id, string Name, decimal Price, string description);
record Salesman(int Id, string Name, string email);
record Sales(int Id, string Shop_Name, string Shop_address, DateTime Sales_date, int Salesman_id, int Products_id);

//##### class for responce object created but not used yet !
public class Responce
{
  public System.Text.Encoding Message { get; set; }
  public string Status { get; set; }
  public Responce() { }
  public Responce(System.Text.Encoding message, string status)
  {
    Message = message;
    Status = status;
  }
}