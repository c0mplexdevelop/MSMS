using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Diagnosis;

namespace MSMS.Controllers;

public class DiagnosisController : Controller
{
    private readonly IPatientDatabaseRepository _patientDb;

    public DiagnosisController(IPatientDatabaseRepository patientDb)
    {
        _patientDb = patientDb;
    }


    public IActionResult MedicalRecords()
    {
        var model = new PatientViewModel();
        return View(model);
    }

    public IActionResult PatientMedicalRecord(int id)
    {
        var model = _patientDb.GetById(id);
        if (model is null)
        {
            return NotFound();
        }
        return View(model);
    }


}
