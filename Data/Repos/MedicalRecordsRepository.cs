using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;

namespace MSMS.Data.Repos;

public class MedicalRecordsRepository : IMedicalRecordsRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger<MedicalRecordsRepository> _logger;

    public MedicalRecordsRepository(DatabaseContext context, ILogger<MedicalRecordsRepository> logger)
    {
        _context = context;
        _logger = logger;
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

    public bool PatientHasRecord(int patientId)
    {
        return _context.MedicalRecords.Any(m => m.PatientId == patientId);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void UpdateExistingModel(MedicalRecord model)
    {
        var existingModel = _context.MedicalRecords.Find(model.Id);
        _logger.LogInformation($"{model.Id}");
        _context.Entry(existingModel).CurrentValues.SetValues(model);
    }
}
