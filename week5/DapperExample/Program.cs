using MySql.Data.MySqlClient;
using Dapper;


// NOTE: Uncomment one of the lines to run the query
//await AdoNetGetAll();
//await DapperGetAll();
//await DapperGetById();
//await DapperAddProduct();
//await DapperUpdateProduct();
//await DapperDeleteProduct();

// Pure ADO.NET

async Task AdoNetGetAll()
{
    List<Product> products = new List<Product>();

    using (var connection = new MySqlConnection(Shared.ConnectionString))
    {
        connection.Open();
        using (var command = new MySqlCommand("SELECT id, name, price FROM products", connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var product = new Product();

                    product.ID = reader.GetInt32(reader.GetOrdinal("id"));
                    product.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString(reader.GetOrdinal("name"));
                    product.Price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("price"));

                    products.Add(product);
                }
            }
        }
    }

    foreach (var product in products)
    {
        Console.WriteLine($"{product.ID} - {product.Name} :: {product.Price}");
    }
}

// Dapper

// ### Get all ###
async Task DapperGetAll()
{
    await using var connection = new MySqlConnection(Shared.ConnectionString);
    var products = await connection.QueryAsync<Product>("SELECT id, name, price FROM products");

    foreach (var product in products)
    {
        Console.WriteLine($"{product.ID} - {product.Name} :: {product.Price}");
    }
}

// ### Get by ID ###
async Task DapperGetById()
{
    await using var connection = new MySqlConnection(Shared.ConnectionString);
    var product = await connection.QueryFirstAsync<Product>("SELECT id, name, price FROM products WHERE id=@CustomId", new { CustomId = 1 });

    Console.WriteLine($"{product.ID} - {product.Name} :: {product.Price}");
}

// ### Insert ###
async Task DapperAddProduct()
{
    await using var connection = new MySqlConnection(Shared.ConnectionString);
    var product = new Product() { Name = "Cargo bike", Price = 15000 };
    var productId = await connection.ExecuteAsync("INSERT INTO products (name, price) VALUES (@name, @price)", product);
}

// ### Update ###
async Task DapperUpdateProduct()
{
    await using var connection = new MySqlConnection(Shared.ConnectionString);
    var product = new Product() { ID = 1, Name = "Tesla", Price = 710000 };
    await connection.ExecuteAsync("UPDATE products SET name=@name, price=@price WHERE id=@id", product);
}

// ### Delete ###
async Task DapperDeleteProduct()
{
    await using var connection = new MySqlConnection(Shared.ConnectionString);
    await connection.ExecuteAsync("DELETE FROM products WHERE id=@id", new { ID=3 });
}