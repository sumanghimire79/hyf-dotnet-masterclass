
using HackYourFuture.Week6;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "week6 homework database !");
app.MapGet("/users", async (IUserRepository userRepository) =>
{
  return await userRepository.GetUsers();
});
app.MapGet("/products", async (IProductRepository productRepository) =>
{
  return await productRepository.GetProducts();
});

app.Run();

