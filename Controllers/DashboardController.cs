using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Payments;

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

    [HttpGet]
    public IActionResult Payments()
    {
        ViewBag.ActiveSection = "Payments";
        var model = new PaymentsViewModel();
        var payments = _paymentDb.GetAll();
        var patients = _patientDb.GetAll();
        if (!payments.Any())
        {
            _logger.LogInformation("No payments found");
        }
        foreach (var payment in payments)
        {
            _logger.LogInformation(payment.ToString() ?? "null");
        }
        model.Payments = payments.ToList();
        model.Patients = patients.ToList();
        _logger.LogInformation($"{model.Payments.Count}");
        return View(model);
    }

    [HttpPost]
    public IActionResult Payments(PaymentsViewModel model)
    {
        _logger.LogInformation($"{model is null}");
        _logger.LogInformation($"{model?.Payments is null}");
        //if (!ModelState.IsValid)
        //{
        //    model = new PaymentsViewModel
        //    {
        //        Payments = payments.ToList()
        //    };

        //    _logger.LogInformation("Model state is invalid");
        //    return View("Payments", model);
        //}
        foreach (var payment in model.Payments)
        {
            _logger.LogInformation($"{payment.PaymentStatus.ToString()}");
            _paymentDb.Update(payment);
        }
        _paymentDb.SaveChanges();
        _logger.LogInformation($"{model.Payments[0].PaymentStatus.ToString()}");
        return View(model);
    }
}
