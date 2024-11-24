using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Diagnosis;

namespace MSMS.Controllers;

public class DiagnosisController : Controller
{
    private readonly ILogger<DiagnosisController> _logger;
    private readonly IPatientDatabaseRepository _patientDb;
    private readonly IDatabaseRepository<Diagnosis> _diagnosisDb;

    public DiagnosisController( ILogger<DiagnosisController> logger, IPatientDatabaseRepository patientDb,
                                IDatabaseRepository<Diagnosis> diagnosisDb)
    {
        _logger = logger;
        _patientDb = patientDb;
        _diagnosisDb = diagnosisDb;
    }


    public IActionResult MedicalRecords()
    {
        var patients = _patientDb.GetAll();
        var model = new PatientViewModel
        {
            Patients = patients.ToList()
        };
        return View(model);
    }

    public IActionResult PatientMedicalRecord(int id)
    {
        var model = _patientDb.GetById(id)!;
        _logger.LogInformation(model.MedicalRecord.Diagnoses!.Count.ToString());
        if (model is null)
        {
            return NotFound();
        }
        model.MedicalRecord.Diagnoses = model.MedicalRecord.Diagnoses.OrderByDescending(d => d.CreatedAt).ToList();
        return View(model);
    }

    [HttpGet]
    public IActionResult DiagnosisForm(int patientId, int diagnosisId)
    {
        _logger.LogInformation($"Diagnosis Form Id: {diagnosisId}");
        Patient patient = _patientDb.GetById(patientId)!;
        var model = new DiagnosisViewModel
        {
            Patient = patient,
            Diagnosis = patient.MedicalRecord.Diagnoses!.FirstOrDefault(d => d.Id == diagnosisId) ?? new Diagnosis()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult ModeledDiagnosisForm(DiagnosisViewModel model)
    {
        return View("DiagnosisForm", model);
    }


    [HttpPost]
    public IActionResult SaveDiagnosis(DiagnosisViewModel model)
    {
        if(!ModelState.IsValid)
        {
            _logger.LogDebug($"Doctor: {model.Patient.MedicalRecord.Doctor}");
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if(state.Errors.Any())
                {
                    foreach (var error in state.Errors)
                    {
                        _logger.LogError($"Class and Property: {key}, ErrorMessage: {error.ErrorMessage}");
                    }
                }
                
            }
            return View(nameof(DiagnosisForm), model);
        }

        if (model.Diagnosis.Id == 0)
        {
            _diagnosisDb.Add(model.Diagnosis);
            _diagnosisDb.SaveChanges();
        }
        else
        {
            var diagnosis = model.Diagnosis;
            diagnosis.DiagnosisDetails = model.Diagnosis.DiagnosisDetails;
            diagnosis.Notes = model.Diagnosis.Notes;
            _diagnosisDb.UpdateExistingModel(diagnosis);
        }
        _logger.LogInformation($"Patient is null: {model.Patient is null}");
        _logger.LogInformation($"Diagnosis Details: {model.Diagnosis.DiagnosisDetails}");
        return RedirectToAction("PatientMedicalRecord", new { id = model.Patient.Id });
        
    }

    [HttpPost]
    public IActionResult AddDiagnosis(int id)
    {
        var patient = _patientDb.GetById(id);
        var diagnosis = new Diagnosis
        {
            Id = 0,
            PatientId = id,
            MedicalRecordId = patient!.MedicalRecord.Id
        };

        var model = new DiagnosisViewModel
        {
            Patient = patient,
            Diagnosis = diagnosis
        };

        return ModeledDiagnosisForm(model);
        
    }

}
