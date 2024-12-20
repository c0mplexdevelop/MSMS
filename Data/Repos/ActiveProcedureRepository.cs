﻿using Microsoft.EntityFrameworkCore;
using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;
using MSMS.Models.Procedures;

namespace MSMS.Data.Repos;

public class ActiveProcedureRepository : IActiveProcedureDatabaseRepository
{
    private readonly DatabaseContext context;

    public ActiveProcedureRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(ActiveProcedure model)
    {
        context.Add(model);
    }

    public void Delete(ActiveProcedure model)
    {
        context.Remove(model);
        context.SaveChanges();
    }

    public IEnumerable<ActiveProcedure> GetAll()
    {
        return context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).ToList();
    }

    public IEnumerable<ActiveProcedure?> GetAllNotPaidProceduresOfPatientByPatientId(int patientid)
    {
        return context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).Where(x => x.Patient.Id == patientid && x.IsPaid == false);
    }

    public IEnumerable<ActiveProcedure?> GetAllPatientsOfProcedureByProcedureId(int id)
    {
        return [.. context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).Where(x => x.Procedure.Id == id)];
    }

    public IEnumerable<ActiveProcedure?> GetAllProceduresOfPatientByPatientId(int id)
    {
        return [.. context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).Where(x => x.Patient.Id == id)];
    }

    public ActiveProcedure? GetById(int id)
    {
        return context.ActiveProcedures.Include(x => x.Patient).Include(x => x.Procedure).FirstOrDefault(x => x.Id == id);
    }

    public ActiveProcedure? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExistingModel(ActiveProcedure model)
    {
        var entry = context.ActiveProcedures.Include(ap => ap.Patient).Include(ap => ap.Procedure).SingleOrDefault(ap => ap.Id == model.Id);
        context.Entry(entry).CurrentValues.SetValues(model);
        context.SaveChanges();
    }
}
