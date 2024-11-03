using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;

namespace MSMS.Controllers;

public class DashboardController : Controller
{
    private ILogger<DashboardController> _logger;

    private IMedicineDatabaseRepository _medicineDb;
    private IPaymentDatabaseRepository _paymentDb;
    private IDatabaseRepository<Supplier> _supplierDb;
    private IPatientDatabaseRepository _patientDb;

    public DashboardController( ILogger<DashboardController> logger, IMedicineDatabaseRepository medicineDb, 
                                IDatabaseRepository<Supplier> supplierDb, IPaymentDatabaseRepository paymentDb,
                                IPatientDatabaseRepository patientDb)
    {
        _logger = logger;
        _medicineDb = medicineDb;
        _supplierDb = supplierDb;
        _paymentDb = paymentDb;
        _patientDb = patientDb;
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

    [HttpPost]
    public IActionResult Inventory(string searchString)
        {
        var medicines = _medicineDb.GetAll();
        var suppliers = _supplierDb.GetAll();

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

    public IActionResult Payments(User user)
    {
        ViewBag.ActiveSection = "Payments";
        return View(user);
    }
}
