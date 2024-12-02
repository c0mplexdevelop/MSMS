using MSMS.Models.Dashboard;
using MSMS.Models.Login;

namespace MSMS.Data.Interfaces;

public interface IUserDatabaseRepository : IDatabaseRepository<User>
{
    User? GetByCredential(UserCredential userCredential);
}
