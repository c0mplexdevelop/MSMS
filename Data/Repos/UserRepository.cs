using Microsoft.EntityFrameworkCore;
using MSMS.Models.Dashboard;
using MSMS.Models.Login;

namespace MSMS.Data.Repos;

public class UserRepository : IUserDatabaseRepository
{

    private readonly DatabaseContext context;

    public UserRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(User model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        {
            return [.. context.Users];
        }
    }

    public User? GetByCredential(UserCredential userCredential)
    {
        return context.Users.FirstOrDefault(u => u.Username == userCredential.Username && u.Password == userCredential.Password);
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public User? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExisitngModel(User model)
    {
        context.Users.Update(model);
    }
}
