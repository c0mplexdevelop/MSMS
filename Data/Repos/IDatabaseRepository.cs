using MSMS.Models.Login;

namespace MSMS.Data.Repos;

public interface IDatabaseRepository<T> where T : class
{
    T GetById(int id);
    T? GetByCredential(UserCredential userCredential);
    IEnumerable<T> GetAll();

}
