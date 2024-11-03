using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;

namespace MSMS.Controllers;

public class DashboardController : Controller
{
    private ILogger<DashboardController> _logger;

    private IMedicineDatabaseRepository _medicineDb;
    private IDatabaseRepository<Supplier> _supplierDb;

    public DashboardController(ILogger<DashboardController> logger, IMedicineDatabaseRepository medicineDb, IDatabaseRepository<Supplier> supplierDb)
    {
        _logger = logger;
        _medicineDb = medicineDb;
        _supplierDb = supplierDb;
    }

    public IActionResult Notifications(User user)
    {
        ViewBag.ActiveSection = "Notifications";
        return View(user);
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
        {
            _logger.LogInformation(supplier.ToString());
        }
        ViewBag.ActiveSection = "Inventory";
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

    public IActionResult Payments(User user)
    {
        ViewBag.ActiveSection = "Payments";
        return View(user);
    }
}
