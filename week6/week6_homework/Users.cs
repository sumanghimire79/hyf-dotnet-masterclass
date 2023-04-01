using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetUsers();
  // Task<IEnumerable<User>> AddUsers();

}

public class User
{

  public string ID { get; set; } = "default id";
  public string Name { get; set; } = "Default name";
  public string Email { get; set; } = "default@gmial.com";
  public string Address { get; set; } = "biratnagar";

  public User() { }
  public User(string id, string name, string email, string address)
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


  public async Task<IEnumerable<User>> AddUsers()
  {
    using var connection = new MySqlConnection(connectionString);

    var users = await connection.QueryAsync<User>("SELECT id as _id, Name as name , Email as email , Address as address FROM week6homework.user");
    return users;
  }

}



