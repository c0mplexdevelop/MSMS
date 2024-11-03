using MSMS.Models.Login;

namespace MSMS.Data.Repos;

public interface IDatabaseRepository<T> where T : class
{
    T? GetById(int id);
    IEnumerable<T> GetAll();
    void Update(T model);
    void SaveChanges();

}
