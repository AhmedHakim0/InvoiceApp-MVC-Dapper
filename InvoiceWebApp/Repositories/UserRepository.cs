
namespace InvoiceWebApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public User? GetUserByUsernameAndPassword(string username, string password)
    {
        var query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

        using var connection = _context.CreateConnection();
        var user = connection.QueryFirstOrDefault<User>(query, new { Username = username, Password = password });
        return user;
    }

    public void AddUser(User user)
    {
        var query = @"INSERT INTO Users (FullName, Username, Email, Password, Phone, IsVerified)
                  VALUES (@FullName, @Username, @Email, @Password, @Phone, @IsVerified)";

        using var connection = _context.CreateConnection();
        connection.Execute(query, user);
    }

    // Check if a username already exists in the database
    public bool UsernameExists(string username)
    {
        var query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";

        using var connection = _context.CreateConnection();
        return connection.ExecuteScalar<bool>(query, new { Username = username });
    }

    // Update the user's verification status by email
    public void VerifyUserByEmail(string email)
    {
        Console.WriteLine("UPDATING USER: " + email);

        var query = "UPDATE Users SET IsVerified = 1 WHERE Email = @Email";
        using var connection = _context.CreateConnection();
        var Rows = connection.Execute(query, new { Email = email });
        Console.WriteLine("ROWS AFFECTED: " + Rows);
    }

}
