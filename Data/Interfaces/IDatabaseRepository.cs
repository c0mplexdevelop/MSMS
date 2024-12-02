using MSMS.Models.Login;

namespace MSMS.Data.Interfaces;

public interface IDatabaseRepository<T> where T : class
{
    T? GetById(int id);
    T? GetByIdWithNoTracking(int id);
    IEnumerable<T> GetAll();
    void UpdateExistingModel(T model);
    void Add(T model);
    void Delete(T model);
    void SaveChanges();

}
