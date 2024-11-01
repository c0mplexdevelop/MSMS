using Microsoft.EntityFrameworkCore;
using MSMS.Models.Dashboard;
using MSMS.Models.Login;

namespace MSMS.Data.Repos;

public class UserRepository : IUserDatabaseRepository
{

    public UserRepository()
    {
        using var context = new DatabaseContext();
        var users = new List<User> {
                new() { Id = 1, Name = "John Doe", Username = "c0mplex", Password = "test123"},
                new() { Id = 2, Name = "Jane Doe" }
            };

        if (context.Users.Any())
        {
            return;
        }
        context.Users.AddRange(users);
        context.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        using(var context = new DatabaseContext())
        {
            return [.. context.Users];
        }
    }

    public User? GetByCredential(UserCredential userCredential)
    {
        using var context = new DatabaseContext();
        return context.Users.FirstOrDefault(u => u.Username == userCredential.Username && u.Password == userCredential.Password);
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }
}
