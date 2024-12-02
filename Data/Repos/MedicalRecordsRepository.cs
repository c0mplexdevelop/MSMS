using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;

namespace MSMS.Data.Repos;

public class MedicalRecordsRepository : IMedicalRecordsRepository
{
    private readonly DatabaseContext _context;

    public MedicalRecordsRepository(DatabaseContext context)
    {
        _context = context;
    }
    public void Add(MedicalRecord model)
    {
        _context.MedicalRecords.Add(model);
    }

    public void Delete(MedicalRecord model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MedicalRecord> GetAll()
    {
        return _context.MedicalRecords;
    }

    public MedicalRecord? GetById(int id)
    {
        return _context.MedicalRecords.Find(id);
    }

    public MedicalRecord? GetByPatientId(int patientId)
    {
        return _context.MedicalRecords.FirstOrDefault(m => m.PatientId == patientId);
    }

    public MedicalRecord? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void UpdateExistingModel(MedicalRecord model)
    {
        throw new NotImplementedException();
    }
}
