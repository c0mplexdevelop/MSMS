using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Data.Interfaces;

public interface IActiveProcedureDatabaseRepository : IDatabaseRepository<ActiveProcedure>
{
    IEnumerable<ActiveProcedure?> GetAllPatientsOfProcedureByProcedureId(int id);
    IEnumerable<ActiveProcedure?> GetAllProceduresOfPatientByPatientId(int id);
    IEnumerable<ActiveProcedure?> GetAllNotPaidProceduresOfPatientByPatientId(int patientid);

}
