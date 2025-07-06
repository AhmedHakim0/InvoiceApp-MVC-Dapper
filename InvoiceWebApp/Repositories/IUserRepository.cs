
namespace InvoiceWebApp.Repositories;

public interface IUserRepository
{
    User? GetUserByUsernameAndPassword(string username, string password);
    void AddUser(User user);
    bool UsernameExists(string username);
    void VerifyUserByEmail(string email);
}
