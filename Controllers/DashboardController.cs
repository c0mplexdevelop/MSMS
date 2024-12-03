using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Interfaces;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Notification;
using MSMS.Services;

namespace MSMS.Controllers;

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
        _logger.LogInformation($"IsAuth: {User.Identity.IsAuthenticated}");

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

    public IActionResult AccessDenied()
    {
        return View();
    }
}
