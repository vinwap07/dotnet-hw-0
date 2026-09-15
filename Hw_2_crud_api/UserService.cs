using DevOne.Security.Cryptography.BCrypt;

namespace Hw_2_crud_api;

public class UserService
{
    private readonly List<User> _users = [];

    public void AddUser(string login, string password)
    {
        if (_users.FirstOrDefault(x => x.Login == login) != null)
        {
            throw new Exception($"User with login {login} already exists.");
        }
        
        string salt = BCryptHelper.GenerateSalt();
        
        var user = new User()
        {
            Login = login,
            PasswordHash = BCryptHelper.HashPassword(password, salt)
        };
        
        _users.Add(user);
    }

    public void UpdateUser(string login, string? newLogin, string? newPassword)
    {
        var user = _users.FirstOrDefault(x => x.Login == login);
        if (user == null)
        {
            throw new Exception($"User with login {login} does not exist.");
        }
        
        string salt = BCryptHelper.GenerateSalt();
        
        user.Login = newLogin ?? user.Login;
        user.PasswordHash = newPassword != null ? BCryptHelper.HashPassword(newPassword, salt) : user.PasswordHash;
    }

    public void DeleteUser(string login)
    {
        var user = _users.FirstOrDefault(x => x.Login == login);
        if (user != null)
        {
            _users.Remove(user);
        }
    }

    public List<string> GetUsers()
    {
        return _users.Select(x => x.Login).ToList();
    }
}