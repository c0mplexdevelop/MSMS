using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Data.Interfaces;

public interface IProcedureDatabaseRepository : IDatabaseRepository<Procedure>
{
    IEnumerable<ActiveProcedure?> GetAllProceduresOfPatient(Patient patient);
    IEnumerable<Procedure?> GetAllProceduresByProcedureName(string name);

}
