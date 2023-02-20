# Cheatsheet

## How to get one functionality (vertical)

- Create a database table
- Create a C# model
- Create an interface
- Create a class implementing the interface
- Register interface and class in Startup.cs
- Create a controller
- Inject interface in constructor
- Create actions and use interface methods in actions

## Async cheatsheet

### Model

```csharp
public class User
{
}
```

### Interface

```csharp
public interface IUserRepository
{
	Task<User> GetUserById(int id);
	Task<IEnumerable<User>> GetUsers();
	Task DeleteUserById(int id);
}
```

### Implementation

```csharp
public class UserRepository : IUserRepository
{
	private readonly _connection...

  	public UserRepository()
	{
		_connection = new ...;
	}

	public async Task<User> GetUserById(int id)
	{
		var user = /* ... get user from database ... */;
		return user;
	}

  	public async Task<IEnumerable<User>> GetUsers()
	{
		var users = /* ... get users from database ... */;
		return users;
	}

  	public async Task DeleteUserById(int id)
	{
		/* ... execute delete operation database ... */

		/* Notice no return statement when just Task! */
	}
}
```

### Registration

```csharp
// in startup.cs
services.AddScoped<IUserRepository, UserRepository>();
```

### Controller

```csharp
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
	private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	[HttpGet]
	public async Task<IEnumerable<User>> GetAll()
	{
		return await _userRepository.GetUsers();
	}

	[HttpGet("{id}")]
	public async Task<User> GetUserById([FromRoute] int id)
	{
		return await _userRepository.GetUserById(int id);
	}

	[HttpDelete("{id}")]
	public async Task DeleteUser(int id)
	{
		return await _userRepository.DeleteUserById(id);
	}
}
```
