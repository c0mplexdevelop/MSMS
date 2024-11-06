using MSMS.Models.Procedures;

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

    public IEnumerable<Patient> GetAll()
    {
        return [.. context.Patients];
    }

    public Patient? GetByContactNumber(string contactNumber)
    {
        return context.Patients.FirstOrDefault(p => p.ContactNumber == contactNumber);
    }

    public Patient? GetById(int id)
    {
        return context.Patients.Find(id);
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

    public void UpdateExisitngModel(Patient model)
    {
        
    }


}
