using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetUsers();
  Task<Object> GetUserByID(int id);
  Task<Object> AddUser(User user);
  Task<Object> DeleteUserByID(int id);
  Task<Object> UpdateUserByID(int id, User user);

}

public class User
{

  public int ID { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Address { get; set; }

  public User() { }
  public User(int id, string name, string email, string address)
  {
    ID = id;
    Name = name;
    Email = email;
    Address = address;
  }
}

public class UserRepository : IUserRepository
{
  private string connectionString;

  public UserRepository(IConfiguration configuration)
  {
    this.connectionString = configuration.GetConnectionString("Default");
  }

  public async Task<IEnumerable<User>> GetUsers()
  {
    using var connection = new MySqlConnection(connectionString);

    var users = await connection.QueryAsync<User>("SELECT id as ID, name as Name , email as Email , address as Address FROM week6homework.user");
    return users;
  }


  public async Task<Object> AddUser(User user)
  {
    using var connection = new MySqlConnection(connectionString);
    var userId = await connection.QuerySingleAsync<int>(@"INSERT INTO week6homework.user (name, email,address) VALUES ( @name, @email, @address);
        SELECT LAST_INSERT_ID();
    ", user);

    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $" product is posted",
      AddedProduct = user,
    });
  }
  public async Task<Object> GetUserByID(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var userById = await connection.QueryAsync<User>($"SELECT id as ID, name as Name , email as Email , address as Address FROM week6homework.user WHERE id={id}");
    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $" The user that has id: {id} is:",
      UserByID = userById,
    });
  }

  public async Task<Object> UpdateUserByID(int id, User user)
  {
    await using var connection = new MySqlConnection(connectionString);
    var userToUpdated = await connection.ExecuteAsync($"UPDATE week6homework.user SET name=@name, email=@email,address=@address WHERE id={id}", user);
    var UpdatedUser = await connection.QueryAsync<User>($"SELECT id, name, email,address FROM week6homework.user where id={id}");
    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $"this user id:{id} is updated",
      UpdatedUser = UpdatedUser,
    });
  }
  public async Task<Object> DeleteUserByID(int id)
  {
    await using var connection = new MySqlConnection(connectionString);
    var deletedUser = await connection.ExecuteAsync("DELETE FROM week6homework.user WHERE id=@id", new { ID = id });

    return Results.Ok(new
    {
      Status = "200 ok",
      Message = $"this user id:{id} is deleted",
      DeletedUser = deletedUser,
    });
  }

}



