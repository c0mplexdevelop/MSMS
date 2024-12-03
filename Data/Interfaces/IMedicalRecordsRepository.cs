using MSMS.Models.Diagnosis;

namespace MSMS.Data.Interfaces;

public interface IMedicalRecordsRepository : IDatabaseRepository<MedicalRecord>
{
    MedicalRecord? GetByPatientId(int patientId);
    bool PatientHasRecord(int patientId);
}
