using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Notification;
using MSMS.Models.Procedures;
using MSMS.Services;

namespace MSMS.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private ILogger<DashboardController> _logger;

    private readonly IMedicineDatabaseRepository _medicineDb;
    private readonly IProcedureDatabaseRepository _procedureDb;
    private readonly IDatabaseRepository<Supplier> _supplierDb;
    private readonly IPatientDatabaseRepository _patientDb;
    private readonly INotificationDatabaseRepository _notificationDb;
    private readonly IActiveProcedureDatabaseRepository _activeProcedureDb;
    private readonly NotificationService _notificationService;
    private readonly UserService _userService;

    public DashboardController(ILogger<DashboardController> logger, IMedicineDatabaseRepository medicineDb,
                                IDatabaseRepository<Supplier> supplierDb, IProcedureDatabaseRepository paymentDb,
                                IPatientDatabaseRepository patientDb, INotificationDatabaseRepository notificationDb,
                                IActiveProcedureDatabaseRepository activeProcedureDb, NotificationService notificationService, 
                                UserService userService)
    {
        _logger = logger;
        _medicineDb = medicineDb;
        _supplierDb = supplierDb;
        _procedureDb = paymentDb;
        _patientDb = patientDb;
        _notificationDb = notificationDb;
        _activeProcedureDb = activeProcedureDb;
        _notificationService = notificationService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Notifications()
    {
        ViewBag.ActiveSection = "Notifications";

        var notifications = _notificationDb.GetAll().OrderByDescending(notif => notif.Id);
        if(!notifications.Any())
        {
            _logger.LogInformation("No notifications found");
        }
        var model = new NotificationViewModel
        {
            Notifications = notifications
        };
        return View(model);
    }

    [Authorize(Roles = "Staff, Admin")]

    public IActionResult Inventory(CombinedMedicineSupplierModel model)
    {
        model.SearchString = (ViewData["SearchString"] as string) ?? "";
        var medicines = _medicineDb.GetAll();



        model.Medicines = medicines;
        _logger.LogInformation(model.SearchString);
        foreach (var medicine in model.Medicines)
        {
            _logger.LogInformation(medicine.ToString());
        }

        //foreach (var supplier in _supplierDb.GetAll())
        //{
        //    _logger.LogInformation(supplier.ToString());
        //}
        ViewBag.ActiveSection = "Inventory";
        return View(model);
    }

    [HttpPost]
    public IActionResult Inventory(string searchString)
    {
        var medicines = _medicineDb.GetAll();
        var suppliers = _supplierDb.GetAll();

        ViewData["SearchString"] = searchString;

        _logger.LogInformation(searchString);

        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            medicines = medicines.Where(medicine => medicine.GenericName.ToLower().Contains(searchString) ||
                                                            medicine.BrandName.ToLower().Contains(searchString));
        }

        var model = new CombinedMedicineSupplierModel
        {
            Medicines = medicines,
            Suppliers = suppliers,
            SearchString = searchString
        };

        return View(model);
    }

    public IActionResult Diagnosis(User user)
    {
        ViewBag.ActiveSection = "Diagnosis";
        return View(user);
    }

    public IActionResult Reports(User user)
    {
        ViewBag.ActiveSection = "Reports";
        return View(user);
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
            """;

            _notificationService.Add(title, stringMessage, NotificationReference.Procedures, _userService.GetUser().Id);

            _procedureDb.UpdateExisitngModel(procedure);
            continue;
        }

        _procedureDb.SaveChanges();

        return View("Procedures", model);
    }

    [HttpGet]
    public IActionResult ActiveProcedures()
    {
        var activeProcedures = _activeProcedureDb.GetAll();
        var viewModel = new ActiveProceduresViewModel
        {
            ActiveProcedures = activeProcedures.ToList()
        };


        return View(viewModel);
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
        foreach(var item in selectList.Items)
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

        foreach (var activeProcedure in model.ActiveProcedures)
        {
            ActiveProcedure? existingModel;
            if ((existingModel = _activeProcedureDb.GetById(activeProcedure.Id)) is null)
            {
                _activeProcedureDb.Add(activeProcedure);
                continue;
            }

            if (existingModel.ProcedureId ==  activeProcedure.ProcedureId && existingModel.ProcedureServiceDateTime == activeProcedure.ProcedureServiceDateTime)
            {
                _logger.LogInformation("No changes detected");
                continue;
            }

            var title = "Active Procedure";
            var stringMessage = @$"""
                UPDATED:    Active Procedure Id: {existingModel.ProcedureId} -> {activeProcedure.ProcedureId}
                            Active Procedure Date: {existingModel.ProcedureServiceDateTime} -> {activeProcedure.ProcedureServiceDateTime}
                """;

            _notificationService.Add(title, stringMessage, NotificationReference.Procedures, _userService.GetUser().Id);

            _activeProcedureDb.UpdateExisitngModel(activeProcedure);
        }

        _activeProcedureDb.SaveChanges();
        return RedirectToAction("Notifications");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
