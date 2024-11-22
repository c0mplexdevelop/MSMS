
using MSMS.Models.Diagnosis;

namespace MSMS.Data.Repos;

public interface IPatientDatabaseRepository : IDatabaseRepository<Patient>
{
    IEnumerable<Patient> GetByLastName(string lastName);
    Patient? GetByContactNumber(string contactNumber);
}
