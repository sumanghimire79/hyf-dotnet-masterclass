
using HackYourFuture.Week6;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "week6 homework database !");

//users endpoint
app.MapGet("/users", async (IUserRepository userRepository) =>
{
  return await userRepository.GetUsers();
});
app.MapGet("/users/{id}", async (IUserRepository userRepository, int id) =>
{
  return await userRepository.GetUserByID(id);
});

app.MapPost("/users", async (IUserRepository userRepository, User user) =>
{
  return await userRepository.AddUser(user);
});
app.MapPut("/users/{id}", async (IUserRepository userRepository, User user, int id) =>
{
  return await userRepository.UpdateUserByID(id, user);
});
app.MapDelete("/users/{id}", async (IUserRepository userRepository, int id) =>

{
  return await userRepository.DeleteUserByID(id);
});


//product endpoint
app.MapGet("/products", async (IProductRepository productRepository) =>
{
  return await productRepository.GetProducts();
});
app.MapPost("/products", async (IProductRepository productRepository, Product product) =>
{
  return await productRepository.AddProducts(product);
});
app.MapGet("/products/{id}", async (IProductRepository productRepository, int id) =>
{
  return await productRepository.GetProductById(id);
});
app.MapPut("/products/{id}", async (IProductRepository productRepository, Product product, int id) =>
{
  return await productRepository.UpdatedProductById(id, product);
});
app.MapDelete("/products/{id}", async (IProductRepository productRepository, int id) =>
{
  return await productRepository.DeleteProductById(id);
});


app.Run();

