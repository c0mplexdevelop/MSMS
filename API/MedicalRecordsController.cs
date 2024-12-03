using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;

namespace MSMS.API;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{

    private readonly IMedicalRecordsRepository _medicalRecordsDb;

    public MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository)
    {
         _medicalRecordsDb = medicalRecordsRepository;
    }

    // GET: api/medicalrecords/getall
    [HttpGet("getall")]
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecord()
    {
        var medicalRecords = _medicalRecordsDb.GetAll();
        return Ok(medicalRecords);
    }

    [HttpGet("getalltest")]
    public ActionResult<int> GetAllMedicalRecordLength()
    {
        var count = _medicalRecordsDb.GetAll().Count();
        return Ok(count);
    }

    // GET: api/MedicalRecords/get/{id}
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
        //return _medicalRecordsDb.MedicalRecords.Find(id);
        return _medicalRecordsDb.GetById(id) ?? null;
    }
}
