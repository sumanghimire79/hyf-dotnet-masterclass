
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IProductRepository
{
  Task<IEnumerable<Product>> GetProducts();
}
public class Product
{
  // public string _ID;
  // public string ID { get => _ID; set => new Guid(); }
  public string ID { get; set; }
  public string? ProductName { get; set; }
  public decimal Price { get; set; }
  public string? Description { get; set; }
  public Product() { }
  public Product(string id, string name, decimal price, string description)
  {
    ID = id;
    ProductName = name;
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

  public async Task<IEnumerable<Product>> GetProducts()
  {
    using var connection = new MySqlConnection(connectionString);

    var products = await connection.QueryAsync<Product>("SELECT id as ID,  name as  ProductName, price as Price , description as Description FROM week6homework.product");
    return products;
  }

}
