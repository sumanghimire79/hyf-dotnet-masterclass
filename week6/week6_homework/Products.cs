
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IProductRepository
{
  Task<Object> AddProducts(Product product);
  Task<IEnumerable<Product>> GetProducts();
  Task<Object> GetProductById(int id);
  Task<Object> DeleteProductById(int id);
  Task<Object> UpdatedProductById(int id, Product product);
}
public class Product
{
  public int ID { get; set; }
  public string? Name { get; set; }
  public decimal Price { get; set; }
  public string? Description { get; set; }
  public Product() { }
  public Product(int id, string name, decimal price, string description)
  {
    ID = id;
    Name = name;
    Price = price;
    Description = description;
  }
}

public class ProductRepository : IProductRepository
{
  private string connectionString;

  public ProductRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }



  public async Task<Object> AddProducts(Product product)
  {
    using var connection = new MySqlConnection(connectionString);
    var productId = await connection.QuerySingleAsync<int>(@"INSERT INTO dapper.products (name, price,description) VALUES (@name, @price, @description);
    SELECT LAST_INSERT_ID();
", product);
    var products = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={productId}");
    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $" product is posted",
      AddedProduct = products,
    });
  }


  public async Task<IEnumerable<Product>> GetProducts()
  {
    using var connection = new MySqlConnection(connectionString);
    var products = await connection.QueryAsync<Product>("SELECT id,  name, price , description FROM dapper.products");
    return products;
  }

  public async Task<Object> GetProductById(int id)
  {
    using var connection = new MySqlConnection(connectionString);
    var productById = await connection.QueryAsync<Product>($"SELECT id, name, price , description FROM dapper.products WHERE id = {id}");
    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $"The product that has id {id} is: ",
      ProductById = productById,
    });
  }

  public async Task<Object> UpdatedProductById(int id, Product product)
  {
    await using var connection = new MySqlConnection(connectionString);
    var productUpdated = await connection.ExecuteAsync($"UPDATE dapper.products SET name=@name, price=@price,description=@description WHERE id={id}", product);
    var UpdatedProduct = await connection.QueryAsync<Product>($"SELECT id, name, price,description FROM dapper.products where id={id}");
    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $"this product id:{id} is updated",
      UpdatedProduct = UpdatedProduct,
    });
  }

  public async Task<Object> DeleteProductById(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var deletedProduct = await connection.ExecuteAsync("DELETE FROM dapper.products WHERE id=@id", new { ID = id });

    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $"this product id:{id} is deleted",
      DeletedProduct = deletedProduct,
    });
  }

}


