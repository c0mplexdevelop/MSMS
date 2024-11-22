using Microsoft.EntityFrameworkCore;
using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Data.Repos;

public class ActiveProcedureRepository : IActiveProcedureDatabaseRepository
{
    private readonly DatabaseContext context;

    public ActiveProcedureRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(ActiveProcedure model)
    {
        context.Add(model);
    }

    public void Delete(ActiveProcedure model)
    {
        context.Remove(model);
        context.SaveChanges();
    }

    public IEnumerable<ActiveProcedure> GetAll()
    {
        return context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).ToList();
    }

    public IEnumerable<ActiveProcedure?> GetAllPatientsOfProcedure(Procedure procedure)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ActiveProcedure?> GetAllProceduresOfPatient(Patient patient)
    {
        throw new NotImplementedException();
    }

    public ActiveProcedure? GetById(int id)
    {
        return context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).FirstOrDefault(x => x.Id == id);
    }

    public ActiveProcedure? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExisitngModel(ActiveProcedure model)
    {
        var entry = context.ActiveProcedures.Include(ap => ap.Patient).Include(ap => ap.Procedure).SingleOrDefault(ap => ap.Id == model.Id);
        context.Entry(entry).CurrentValues.SetValues(model);
        context.SaveChanges();
    }
}
