using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSMS.Data.Repos;
using MSMS.Models.Notification;
using MSMS.Models.Procedures;
using MSMS.Services;
using Newtonsoft.Json;

namespace MSMS.Controllers;

public class ProcedureController : Controller
{
    private readonly IProcedureDatabaseRepository _procedureDb;
    private readonly IActiveProcedureDatabaseRepository _activeProcedureDb;
    private readonly IPatientDatabaseRepository _patientDb;

    private readonly ILogger<ProcedureController> _logger;

    private readonly UserService _userService;
    private readonly NotificationService _notificationService;

    public ProcedureController( IProcedureDatabaseRepository procedureDb, IActiveProcedureDatabaseRepository activeProcedureDb,
                                IPatientDatabaseRepository patientDb, ILogger<ProcedureController> logger,
                                UserService userService, NotificationService notificationService)
    {
        _procedureDb = procedureDb;
        _activeProcedureDb = activeProcedureDb;
        _patientDb = patientDb;
        _logger = logger;
        _userService = userService;
        _notificationService = notificationService;
    }

    [HttpGet]
    public IActionResult Procedures()
    {
        ViewBag.ActiveSection = "Procedures";
        var model = new ProceduresViewModel();
        var payments = _procedureDb.GetAll();
        var patients = _patientDb.GetAll();
        if (!payments.Any())
        {
            _logger.LogInformation("No payments found");
        }
        foreach (var payment in payments)
        {
            _logger.LogInformation(payment.ToString() ?? "null");
        }
        model.Procedures = payments.ToList();
        model.Patients = patients.ToList();
        _logger.LogInformation($"{model.Procedures.Count}");
        return View(model);
    }

    [HttpPost]
    public IActionResult SearchProcedures(string searchString)
    {
        ViewData["SearchString"] = searchString;
        var procedures = _procedureDb.GetAll();

        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            procedures = procedures.Where(prod => prod.ProcedureName.ToLower() == searchString || prod.ProcedurePrice == Decimal.Parse(searchString));
        }

        var model = new ProceduresViewModel
        {
            Procedures = procedures.ToList()
        };
        return View("Procedures", model);
    }

    [HttpPost]
    public IActionResult AddProcedure(int addAmount)
    {
        var patients = _patientDb.GetAll();
        var procedures = _procedureDb.GetAll().ToList();
        int lastId = procedures[procedures.Count() - 1].Id;


        if (addAmount <= 1)
        {
            procedures.Add(new Procedure
            {
                Id = lastId + 1
            });
        }
        else
        {
            for (int i = 0; i < addAmount; i++)
            {
                procedures.Add(new Procedure
                {
                    Id = lastId + 1
                });
                lastId++;
            }
        }

        _logger.LogInformation(addAmount.ToString() ?? "null");

        var model = new ProceduresViewModel
        {
            Patients = patients.ToList(),
            Procedures = procedures
        };
        return View("Procedures", model);
    }

    [HttpGet]
    public IActionResult EditProcedures()
    {
        var model = new ProceduresViewModel();
        var procedures = _procedureDb.GetAll();
        var patients = _patientDb.GetAll();

        model.Procedures = procedures.ToList();
        model.Patients = patients.ToList();

        return View(model);
    }

    [HttpPost]
    public IActionResult SaveProcedures(ProceduresViewModel model)
    {
        var procedures = _procedureDb.GetAll().ToList();
        foreach (var procedure in model.Procedures)
        {
            Procedure? existingModel;
            if ((existingModel = _procedureDb.GetById(procedure.Id)) is null)
            {
                _procedureDb.Add(procedure);
                continue;
            }

            if (existingModel == procedure)
            {
                continue;
            }


            var title = "Procedure";
            var stringMessage = @$"""
                UPDATED:    {existingModel.ProcedureName} -> {procedure.ProcedureName} 
                            {existingModel.ProcedurePrice} -> {procedure.ProcedurePrice}
                            {existingModel.ProcedureDescription} -> {procedure.ProcedureDescription}
                            
                            {existingModel.ProcedureNotes} -> {procedure.ProcedureNotes}
            ""
            ";

            _notificationService.Add(title, stringMessage, NotificationReference.Procedures, _userService.GetUser().Id);

            _procedureDb.UpdateExisitngModel(procedure);
            continue;
        }

        _procedureDb.SaveChanges();

        //return View("Procedures", model);
        return RedirectToAction("Notifications", "Dashboard");  
    }

    [HttpGet]
    public IActionResult ActiveProcedures()
    {
        var activeProcedures = _activeProcedureDb.GetAll();
        var viewModel = new ActiveProceduresViewModel
        {
            ActiveProcedures = activeProcedures.ToList()
        };

        ViewBag.ActiveSection = "ActiveProcedures";
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SearchActiveProcedures(string searchString)
    {
        ViewData["SearchString"] = searchString;
        var activeProcedures = _activeProcedureDb.GetAll();

        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            Decimal searchDecimal;
            DateTime searchDateTime;
            if (Decimal.TryParse(searchString, out searchDecimal)) {
                activeProcedures = activeProcedures.Where(prod => prod.Procedure.ProcedurePrice == searchDecimal);
            } else if (DateTime.TryParse(searchString, out searchDateTime))
            {
                activeProcedures = activeProcedures.Where(prod => prod.ProcedureServiceDateTime.Date == searchDateTime.Date);
            } else
            {
                activeProcedures = activeProcedures.Where(prod => prod.Patient.FullName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                                                                  prod.Procedure.ProcedureName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
                //prod.Procedure.ProcedurePrice == Decimal.TryParse(searchString) ||
            }

        }

        var model = new ActiveProceduresViewModel
        {
            ActiveProcedures = activeProcedures.ToList()
        };
        return View("ActiveProcedures", model);
    }

    [HttpPost]
    public IActionResult AddActiveProcedures(int addAmount)
    {
        GetPatientById(1);
        var procedures = _procedureDb.GetAll();
        _logger.LogInformation($"Procedures: {procedures.Count()}");
        var patients = _patientDb.GetAll();
        var activeProcedures = _activeProcedureDb.GetAll().ToList();
        int lastId = activeProcedures.Count();

        if (addAmount <= 1)
        {
            activeProcedures.Add(
                new ActiveProcedure
                {
                    Id = lastId + 1
                }
            );
        }
        else
        {
            for (int i = 0; i < addAmount; i++)
            {
                activeProcedures.Add(
                    new ActiveProcedure
                    {
                        Id = lastId + 1
                    }
                );
                lastId++;
            }
        }

        var procedureSelectList = new SelectList(procedures, "Id", "Id");
        var patientSelectList = new SelectList(patients, "Id", "Id");
        ViewBag.PatientSelectList = patientSelectList;
        ViewBag.ProcedureSelectList = procedureSelectList;
        var viewModel = new ActiveProceduresViewModel
        {
            ActiveProcedures = activeProcedures
        };

        return View("AddActiveProcedures", viewModel);
    }

    [HttpGet]
    public IActionResult EditActiveProcedures()
    {
        var prodecureIdList = _procedureDb.GetAll();
        var activeProcedures = _activeProcedureDb.GetAll();
        _logger.LogInformation($"Active Procedures: {activeProcedures.Count()}");
        foreach (var ap in activeProcedures)
        {
            _logger.LogInformation($"Procedure Id: {ap.ProcedureId}, Patient Id: {ap.PatientId}");
            _logger.LogInformation($"Patient Name: {ap.Patient.FullName ?? "Null!"}");

        }

        ActiveProceduresViewModel viewModel = new ActiveProceduresViewModel
        {
            ActiveProcedures = activeProcedures.ToList()
        };
        var selectList = new SelectList(prodecureIdList, "Id", "ProcedureName");
        foreach (var item in selectList.Items)
        {
            _logger.LogInformation($"Procedure Id List: {item.ToString()}");

        }

        ViewBag.ProcedureIdList = selectList;

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SaveActiveProcedures(ActiveProceduresViewModel model)
    {
        var activeProcedures = _activeProcedureDb.GetAll().ToList();
        _logger.LogInformation($"Active Procedures: {model.ActiveProcedures.Count()}");

        var title = "Active Procedure";

        foreach (var activeProcedure in model.ActiveProcedures)
        {
            _logger.LogInformation($"Active Procedure Id: {activeProcedure.Id}");
            ActiveProcedure? existingModel = _activeProcedureDb.GetById(activeProcedure.Id);
            if (existingModel is null)
            {

                activeProcedure.Patient = _patientDb.GetById(activeProcedure.PatientId)!;
                activeProcedure.Procedure = _procedureDb.GetById(activeProcedure.ProcedureId)!;
                _logger.LogInformation("Adding new active procedure");
                var notificationMessage = $@"
                    ADDED:      Active Procedure Id: {activeProcedure.Id}
                                    Patient Id: {activeProcedure.PatientId}
                                    Patient Name: {activeProcedure.Patient.FullName}
                                    Procedure Id: {activeProcedure.ProcedureId}
                                    Procedure Name: {activeProcedure.Procedure.ProcedureName}
                                    Procedure Price: {activeProcedure.Procedure.ProcedurePrice}
                                    Active Procedure Date: {activeProcedure.ProcedureServiceDateTime}
                ";
                _activeProcedureDb.Add(activeProcedure);
                _notificationService.Add(title, notificationMessage, NotificationReference.ActiveProcedures, _userService.GetUser().Id);
                continue;
            }

            if (existingModel == activeProcedure)
            {
                continue;
            }

            var notificationList = new List<string>();
            notificationList.Add("UPDATED: ");
            if(activeProcedure.PatientId != existingModel.PatientId)
            {
                activeProcedure.Patient = _patientDb.GetById(activeProcedure.PatientId)!;
                notificationList.Add($@"
                    Patient Id: {existingModel.PatientId} -> {activeProcedure.PatientId}
                    Patient Name: {existingModel.Patient.FullName} -> {activeProcedure.Patient.FullName}    
                ");
            }

            if (activeProcedure.ProcedureId != existingModel.ProcedureId)
            {
                activeProcedure.Procedure = _procedureDb.GetById(activeProcedure.ProcedureId)!;
                notificationList.Add($@"
                    Procedure Id: {existingModel.ProcedureId} -> {activeProcedure.ProcedureId}
                    Procedure Name: {existingModel.Procedure.ProcedureName} -> {activeProcedure.Procedure.ProcedureName}
                    Procedure Price: {existingModel.Procedure.ProcedurePrice} -> {activeProcedure.Procedure.ProcedurePrice}
                ");
            }

            if(!activeProcedure.ProcedureServiceDateTime.Equals(existingModel.ProcedureServiceDateTime))
            {
                notificationList.Add($@"
                    Active Procedure Date: {existingModel.ProcedureServiceDateTime} -> {activeProcedure.ProcedureServiceDateTime}
                ");
            }

            if(notificationList.Any())
            {
                var stringMessage = string.Join("\n", notificationList);
                _notificationService.Add(title, stringMessage, NotificationReference.ActiveProcedures, _userService.GetUser().Id);
            }

            _activeProcedureDb.UpdateExisitngModel(activeProcedure);
        }

        _activeProcedureDb.SaveChanges();
        return RedirectToAction("Notifications", "Dashboard");
    }

    [HttpGet]
    public IActionResult GetPatientById(int id)
    {
        var patient = _patientDb.GetById(id);
        var json = Json(patient);
        _logger.LogInformation(JsonConvert.SerializeObject(json.Value));
        return json;
    }

    [HttpGet]
    public IActionResult GetProcedureById(int id)
    {
        var procedure = _procedureDb.GetById(id);
        var json = Json(procedure);
        _logger.LogInformation(JsonConvert.SerializeObject(json.Value));
        return json;
    }
}
