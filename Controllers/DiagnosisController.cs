using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Interfaces;
using MSMS.Models.Diagnosis;
using MSMS.Services;

namespace MSMS.Controllers;

public class DiagnosisController : Controller
{
    private readonly ILogger<DiagnosisController> _logger;
    private readonly IPatientDatabaseRepository _patientDb;
    private readonly IDatabaseRepository<Diagnosis> _diagnosisDb;
    private readonly IMedicalRecordsRepository _medicalRecordsDb;
    private readonly PatientServices _patientServices;

    private static IEnumerable<Patient> _pmsPatients = new List<Patient>();

    public DiagnosisController( ILogger<DiagnosisController> logger, IPatientDatabaseRepository patientDb,
                                IDatabaseRepository<Diagnosis> diagnosisDb, PatientServices patientServices,
                                IMedicalRecordsRepository medicalRecordsDb)
    {
        _logger = logger;
        _patientDb = patientDb;
        _diagnosisDb = diagnosisDb;
        _patientServices = patientServices;
        _medicalRecordsDb = medicalRecordsDb;
    }


    public async Task<IActionResult> MedicalRecords()
    {
        ViewBag.ActiveSection = "Diagnosis";
        //if(_pmsPatients.Count() < 1)
        //{ 
        //    _pmsPatients = await _patientServices.GetAllPatients();
        //}

        //var model = new PatientViewModel
        //{
        //    Patients = _pmsPatients.ToList()
        //};

        var model = new PatientViewModel
        {
            Patients = _patientDb.GetAll().ToList()
        };
        return View(model);
    }

    public IActionResult PatientMedicalRecord(int id)
    {
        //var model = _pmsPatients.First(p => p.Id == id);
        var model = _patientDb.GetById(id);
        if (model is null)
        {
            return NotFound();
        }

        var patientHasRecord = _medicalRecordsDb.PatientHasRecord(id);
        if (patientHasRecord)
        {
            _logger.LogInformation("Patient has record");
            model.MedicalRecord = _medicalRecordsDb.GetByPatientId(id)!;
        } else if(model.MedicalRecord is null && !patientHasRecord)
        {
            _logger.LogInformation("Patient has no record");
            var medicalRecord = new MedicalRecord
            {
                PatientId = model.Id,

                Doctor = ""
            };

            _medicalRecordsDb.Add(medicalRecord);
            _medicalRecordsDb.SaveChanges();
            model.MedicalRecord = medicalRecord;
        }
        if(model.MedicalRecord!.Diagnoses is null)
        {
            model.MedicalRecord.Diagnoses = new List<Diagnosis>();
            
        }
        _logger.LogInformation(model.MedicalRecord.Diagnoses?.Count.ToString());

        model.MedicalRecord.Diagnoses = model.MedicalRecord.Diagnoses!.OrderByDescending(d => d.CreatedAt).ToList();
        return View(model);
    }

    [HttpPost]
    public IActionResult SaveMedicalRecord(Patient model)
    {
        var medicalRecord = model.MedicalRecord;
        medicalRecord!.Doctor = model.MedicalRecord.Doctor;
        _logger.LogInformation("Saving..");
        _medicalRecordsDb.UpdateExistingModel(medicalRecord);
        _logger.LogInformation("Saved!");
        _medicalRecordsDb.SaveChanges();
        return RedirectToAction("PatientMedicalRecord", new { id = model.Id });
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
