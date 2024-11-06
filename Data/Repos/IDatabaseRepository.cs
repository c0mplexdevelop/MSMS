﻿using MSMS.Models.Login;

namespace MSMS.Data.Repos;

public interface IDatabaseRepository<T> where T : class
{
    T? GetById(int id);
    T? GetByIdWithNoTracking(int id);
    IEnumerable<T> GetAll();
    void UpdateExisitngModel(T model);
    void Add(T model);
    void SaveChanges();

}
