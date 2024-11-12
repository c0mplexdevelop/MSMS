using MSMS.Models.Procedures;

namespace MSMS.Data.Repos;

public interface IActiveProcedureDatabaseRepository : IDatabaseRepository<ActiveProcedure>
{
    IEnumerable<ActiveProcedure?> GetAllPatientsOfProcedure(Procedure procedure);
    IEnumerable<ActiveProcedure?> GetAllProceduresOfPatient(Patient patient);
}
