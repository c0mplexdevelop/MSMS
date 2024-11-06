﻿using Microsoft.EntityFrameworkCore;
using MSMS.Models.Procedures;

namespace MSMS.Data.Repos;

public class ProcedureRepository : IProcedureDatabaseRepository
{
    private readonly DatabaseContext context;

    public ProcedureRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(Procedure model)
    {
        context.Add(model);
    }

    public IEnumerable<Procedure> GetAll()
    {
        return [.. context.Procedure];
    }

    public IEnumerable<Procedure?> GetAllProceduresByProcedureName(string name)
    {
        return context.Procedure.Where(p => p.ProcedureName == name);
    }

    public IEnumerable<ActiveProcedure?> GetAllProceduresOfPatient(Patient patient)
    {
        return context.PatientProcedures.Where(ap => ap.Patient.Id == patient.Id);
    }

    public Procedure? GetById(int id)
    {
        return context.Procedure.Find(id);
    }

    public Procedure? GetByIdWithNoTracking(int id)
    {
        return context.Procedure.AsNoTracking().FirstOrDefault(p => p.Id == id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExisitngModel(Procedure model)
    {
        // So when we submit the form, the model is not being tracked by the context
        // This calls the context and attaches the exisitng model in the context for update
        // Set the current value within the context to the passed model.
        // Update!
        var entry = context.Procedure.Find(model.Id)!;
        context.Entry(entry).CurrentValues.SetValues(model);
        
    }

}

    
