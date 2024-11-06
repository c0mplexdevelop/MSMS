using MSMS.Models.Procedures;

namespace MSMS.Data.Repos;

public interface IProcedureDatabaseRepository : IDatabaseRepository<Procedure>
{
    IEnumerable<ActiveProcedure?> GetAllProceduresOfPatient(Patient patient);
    IEnumerable<Procedure?> GetAllProceduresByProcedureName(string name);

}
