using Microsoft.EntityFrameworkCore;
using MSMS.Models.Diagnosis;

namespace MSMS.Data.Repos;

public class PatientRepository : IPatientDatabaseRepository
{
    private readonly DatabaseContext context;
    public PatientRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(Patient model)
    {
        throw new NotImplementedException();
    }

    public void Delete(Patient model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Patient> GetAll()
    {
        return [.. context.Patients.Include(p => p.MedicalRecord)];
    }

    public Patient? GetByContactNumber(string contactNumber)
    {
        return context.Patients.FirstOrDefault(p => p.ContactNumber == contactNumber);
    }

    public Patient? GetById(int id)
    {
        return context.Patients.Include(p => p.MedicalRecord).ThenInclude(mr => mr.Diagnoses).First(p => p.Id == id);
    }

    public Patient? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Patient> GetByLastName(string lastName)
    {
        return context.Patients.Where(context => context.LastName == lastName);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExistingModel(Patient model)
    {
        var entry = context.Procedure.Find(model.Id)!;
        context.Entry(entry).CurrentValues.SetValues(model);
        SaveChanges();
    }


}
