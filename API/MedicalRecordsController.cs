using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;

namespace MSMS.API;

[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{

    private readonly IMedicalRecordsRepository _medicalRecordsDb;

    public MedicalRecordsController(IMedicalRecordsRepository patientDb)
    {
         _medicalRecordsDb = patientDb;
    }

    [HttpGet("getall")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecord()
    {
        var activeProcedures = _medicalRecordsDb.GetAll();
        return Ok(activeProcedures);
    }

    [HttpGet("get/{id}")]
    public ActionResult<MedicalRecord?> GetMedicalRecord(int id)
    {
        //var activeProcedure = _patientDb.GetById(id);
        //if (activeProcedure is null)
        //{
        //    return NotFound();
        //}

        var medicalRecord = GetMedicalRecordOfPatientById(id);
        if (medicalRecord is null)
        {
            return NotFound();
        }
        return Ok(medicalRecord);
    }

    private MedicalRecord? GetMedicalRecordOfPatientById(int id)
    {
        return _medicalRecordsDb.GetById(id) ?? null;
    }
}
