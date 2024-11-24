using MSMS.Models.Diagnosis;

namespace MSMS.Data.Repos;

public class DiagnosisRepository : IDatabaseRepository<Diagnosis>
{
    private readonly DatabaseContext context;
    private readonly ILogger<DiagnosisRepository> logger;

    public DiagnosisRepository(DatabaseContext context, ILogger<DiagnosisRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    public void Add(Diagnosis model)
    {
        context.Diagnoses.Add(model);
    }

    public void Delete(Diagnosis model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Diagnosis> GetAll()
    {
        throw new NotImplementedException();
    }

    public Diagnosis? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Diagnosis? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExistingModel(Diagnosis model)
    {

        var entry = context.Diagnoses.Find(model.Id)!;
        context.Entry(entry).CurrentValues.SetValues(model);
        logger.LogInformation($"Entry Details: {entry}");
        SaveChanges();
    }
}
