using Microsoft.EntityFrameworkCore;
using MSMS.Data.Interfaces;
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

    public void Add(Account model)
    {
        throw new NotImplementedException();
    }

    public void Delete(Account model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Account> GetAll()
    {
        {
            return [.. context.Accounts];
        }
    }

    public Account? GetByCredential(UserCredential userCredential)
    {
        return context.Accounts.Where(user => user.Username == userCredential.Username && user.Password == userCredential.Password).AsNoTracking().FirstOrDefault();
    }

    public Account GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Account? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExistingModel(Account model)
    {
        context.Accounts.Update(model);
    }
}
