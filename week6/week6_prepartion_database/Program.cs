using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapGet("/", () => "week6 database preparation!");

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
        if (users.Count() != 0)
        {
          return Results.Ok(new Returnobj()
          {
            Status = "200 ok",
            Message = "successful",
            Data = users,
          });
        }
        return Results.Ok(new Returnobj()
        {
          Status = "!!",
          Message = "not found",
          Data = "",
        });
      }
    }
  }
});

app.MapGet("/users2", async (IConfiguration configuration) =>
{
  try
  {
    // starting from C# 8 you no longer need inner {}
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var user = await connection.QueryAsync<User>("SELECT user,host FROM mysql.user"); //select * gives error without empty construtor. 
    if (user.Count() != 0)
    {
      return Results.Ok(new Returnobj()
      {
        Status = "200 ok",
        Message = "successful",
        Data = user,
      });
    }
    return Results.Ok(new Returnobj()
    {
      Status = "!!",
      Message = "not found",
      Data = "",
    });
  }
  catch (System.Exception error)
  {
    throw new Exception($"The error is {error}");
  }
});

// CRUD operations for product table as below: 
app.MapGet("/products", async (IConfiguration configuration) =>
{
  try
  {
    using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
    var products = await connection.QueryAsync<Product>("SELECT id, name, price,description FROM dapper.products");
    if (products.Count() != 0)
    {
      return Results.Ok(new Returnobj()
      {
        Status = "200 ok",
        Message = "successful",
        Data = products,
      });
    }
    return Results.Ok(new Returnobj()
    {
      Status = "!!",
      Message = "not found",
      Data = "",
    });
  }
  catch (System.Exception error)
  {
    throw new Exception($"error is : {error}");
  }
});

app.MapGet("/products/{_id}", async (IConfiguration configuration, string _id) =>
{
  try
  {
    if (int.TryParse(_id, out int id))
    {
      if (id < 0)
      {
        return Results.BadRequest(new Returnobj()
        {
          Status = "400 BadRequest",
          Message = $" you entered id as: '{id}', it can not be negative",
          Data = "[]",
        });
      }
      using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
      var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={id}");
      if (products.Count() == 0)
      {
        return Results.NotFound(new Returnobj()
        {
          Status = "404 Not Found",
          Message = $"The  product that has id:{id} does not exists in the database !!",
          Data = "[]",
        });
      }
      return Results.Ok(new Returnobj()
      {
        Status = "200 ok",
        Message = "successful",
        Data = products,
      });
    }
    else
    {
      return Results.BadRequest(new Returnobj()
      {
        Status = "400 BadRequest",
        Message = $"you entered id as: '{_id}', it can not be string",
        Data = "[]",
      });
    }
  }
  catch (System.Exception error)
  {
    throw new Exception($" yout get the system eroor. The error is: {error}");
  }
});

/* .FirstOrDefault it returns {}, instead of a list of obj. [{},{}]  */
app.MapGet("/firstordefault/{_id}", async (IConfiguration configuration, string _id) =>
{
  try
  {
    if (int.TryParse(_id, out int id))
    {
      if (id < 0)
      {
        return Results.BadRequest(new Returnobj()
        {
          Status = "400 BadRequest",
          Message = $" you entered id as: '{id}', it can not be negative",
          Data = "[]",
        });
      }
      using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
      var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={id}");
      if (products.Count() == 0)
      {
        return Results.NotFound(new Returnobj()
        {
          Status = "404 NotFound",
          Message = $"you entered id '{id}', does not exists in database",
          Data = "[]",
        });
      }
      return Results.Ok(new Returnobj()
      {
        Status = "200 ok",
        Message = "successful",
        Data = products.FirstOrDefault(),
      });
    }
    else
    {
      return Results.BadRequest(new Returnobj()
      {
        Status = "400 BadRequest",
        Message = $"you entered id as: '{_id}', it can not be string",
        Data = "[]",
      });
    }
  }
  catch (System.Exception error)
  {
    throw new Exception($" yout get the system eroor. The error is: {error}");
  }
});

app.MapPost("/products", async (IConfiguration configuration, Product product) =>
{
  /*
  <note> 
  # deciml and decimal? are different. decimal? accepts it to be both either 0 or null.
  # <Nullable<decimal> and decimal? are same thing
  </note>
  <question> 
   but how to check if the price is entered as text ? if needed, how to tryparse any of the value that this coming in body of the product??? 
  </question>
   */
  if (string.IsNullOrEmpty(product.Name))
  {
    return Results.BadRequest(new Returnobj()
    {
      Status = "400 BadRequest",
      Message = $" name: '{product.Name}' is not valid, please provide valid data !!",
      Data = "[]",
    });
  }
  if (product.Price < 0)
  {
    return Results.BadRequest(new Returnobj()
    {
      Status = "400 BadRequest",
      Message = $" price: '{product.Price}' is not valid, it can not be negative !!",
      Data = "[]",
    });
  }
  await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var productId = await connection.QuerySingleAsync<int>(@"INSERT INTO dapper.products (name, price,description) VALUES (@name, @price, @description);
    SELECT LAST_INSERT_ID();
", product);
  var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={productId}");
  return Results.Created($"/products/{productId}", new Returnobj()
  {
    Status = "201 created",
    Message = $"/products/{productId} is created !!",
    Data = products,
  });
});

app.MapPut("/products/{_id}", async (IConfiguration configuration, Product product, string _id) =>
{
  try
  {
    if (int.TryParse(_id, out int id))
    {
      if (string.IsNullOrEmpty(product.Name))
      {
        return Results.BadRequest(new Returnobj()
        {
          Status = "400 BadRequest",
          Message = $" '{product.Name}' or '{product.Price}' is not valid, please provide valid data !!",
          Data = "[]",
        });
      }
      await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
      var productUpdated = await connection.ExecuteAsync($"UPDATE dapper.products SET name=@name, price=@price,description=@description WHERE id={id}", product);
      if (productUpdated == 0)
      {
        return Results.NotFound(new Returnobj()
        {
          Status = "404 not found",
          Message = $"this id: {id} not found",
          Data = "[]",
        });
      }
      return Results.Accepted($"/product/{id}", new Returnobj()
      {
        Status = "2o2 accepted",
        Message = $"/product/{id}   created",
        Data = product,
      });
    }
    return Results.BadRequest(new Returnobj()
    {
      Status = "400 BadRequest",
      Message = $"{_id} id can not be string",
      Data = "[]",
    });
  }
  catch (System.Exception error)
  {
    throw new Exception($"The erooro is : {error} ");
  }

});

app.MapDelete("/products/{_id}", async (IConfiguration configuration, string _id) =>
{
  try
  {
    if (int.TryParse(_id, out int id))
    {
      if (id < 0)
      {
        return Results.BadRequest(new Returnobj()
        {
          Status = "400 BadRequest",
          Message = $" id: '{id}' it can not be negative !!",
          Data = "[]",
        });
      }
      await using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
      var deletedProduct = await connection.ExecuteAsync("DELETE FROM dapper.products WHERE id=@id", new { ID = id });//#####
      if (deletedProduct == 0)
      {
        return Results.NotFound(new Returnobj()
        {
          Status = "404 BadRequest",
          Message = $"this product id:{id} not found",
          Data = deletedProduct,
        });
      }
      return Results.Ok(new Returnobj()
      {
        Status = "200 ok",
        Message = $"this product id:{id} is deleted",
        Data = deletedProduct,
      });
    }
    else
    {
      return Results.BadRequest(new Returnobj()
      {
        Status = "400 BadRequest",
        Message = $"you entered id as: '{_id}', it can not be string",
        Data = "[]",
      });
    }
  }
  catch (System.Exception eroor)
  {
    throw new Exception($"The errror is : {eroor}");
  }
});

// table salesman
app.MapGet("/salesman", async (IConfiguration configuration) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var salesman = await connection.QueryAsync<Salesman>("SELECT dapper.salesman.id as Id, dapper.salesman.name as Salesman_Name,  dapper.salesman.email FROM dapper.salesman");
  if (salesman.Count() == 0)
  {
    return Results.NotFound(new Returnobj()
    {
      Status = "404 BadRequest",
      Message = $"no record found",
      Data = salesman,
    });
  }
  return Results.Ok(new Returnobj()
  {
    Status = "200 ok",
    Message = $" record is fetched",
    Data = salesman,
  });

});

// table sales
app.MapGet("/sales", async (IConfiguration configuration) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var sales = await connection.QueryAsync<Sales>("SELECT *  FROM dapper.sales ");
  if (sales.Count() == 0)
  {
    return Results.NotFound(new Returnobj()
    {
      Status = "404 BadRequest",
      Message = $"no  record found",
      Data = sales,
    });
  }
  return Results.Ok(new Returnobj()
  {
    Status = "200 ok",
    Message = $" record is fetched",
    Data = sales,
  });
});

// join all three tables products,sales and salesman using innerjoin 
app.MapGet("/join", async (IConfiguration configuration) =>
{
  using var connection = new MySqlConnection(configuration.GetConnectionString("Default"));
  var product_salesman_sales = await connection.QueryAsync<JoinedTable>("SELECT dapper.products.id, dapper.products.name,  dapper.sales.shop_name , dapper.salesman.name as salesman_name FROM dapper.products inner join dapper.sales on dapper.products.id = dapper.sales.products_id inner join  dapper.salesman on dapper.salesman.id = dapper.sales.salesman_id ");
  if (product_salesman_sales.Count() == 0)
  {
    return Results.NotFound(new Returnobj()
    {
      Status = "404 BadRequest",
      Message = $"no record found",
      Data = product_salesman_sales,
    });
  }
  return Results.Ok(new Returnobj()
  {
    Status = "200 ok",
    Message = $" data is fetched from joined all three tables products,sales and salesman using sql-dapper innerjoin ",
    Data = product_salesman_sales,
  });
});

app.Run();

// public record User(string user, string host)
// { public User() : this("", "") { } }
public class User
{
  public string? user { get; set; } = "suman";
  public string? host { get; set; } = "ghimire";
  // public User() { }// shows error in /user2 endpoint without this empty constructor in case it is used 'select * '. this is because User class has only two fields user and host.
  public User(string user, string host)
  {
    this.user = user;
    this.host = host;
  }
}

record Product(int Id, string Name, decimal? Price, string description);
record Sales(int Id, string Shop_Name, string Shop_address, DateTime Sales_date, int Salesman_id, int Products_id);
record Salesman(int Id, string Salesman_Name, string email);
record JoinedTable(int Id, string Name, string Shop_Name, string Salesman_Name);

public class Returnobj
{
  public string Status { get; set; }
  public string Message { get; set; }
  public object Data { get; set; }
  public Returnobj(string status, string message, object data)
  {
    this.Status = status;
    this.Message = message;
    this.Data = data;
  }
  public Returnobj() { }
}