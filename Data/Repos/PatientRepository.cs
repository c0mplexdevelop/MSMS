using MSMS.Models.Payments;

namespace MSMS.Data.Repos;

public class PatientRepository : IPatientDatabaseRepository
{
    private readonly DatabaseContext context;
    public PatientRepository(DatabaseContext context)
    {
        this.context = context;
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

    public IEnumerable<Patient> GetByLastName(string lastName)
    {
        return context.Patients.Where(context => context.LastName == lastName);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Update(Patient model)
    {
        context.Patients.Update(model);
    }


}
