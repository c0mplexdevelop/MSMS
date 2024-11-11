using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Procedures;

namespace MSMS.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private ILogger<DashboardController> _logger;

    private IMedicineDatabaseRepository _medicineDb;
    private IProcedureDatabaseRepository _procedureDb;
    private IDatabaseRepository<Supplier> _supplierDb;
    private IPatientDatabaseRepository _patientDb;

    public DashboardController(ILogger<DashboardController> logger, IMedicineDatabaseRepository medicineDb,
                                IDatabaseRepository<Supplier> supplierDb, IProcedureDatabaseRepository paymentDb,
                                IPatientDatabaseRepository patientDb)
    {
        _logger = logger;
        _medicineDb = medicineDb;
        _supplierDb = supplierDb;
        _procedureDb = paymentDb;
        _patientDb = patientDb;
    }

    public IActionResult Notifications(User user)
    {
        ViewBag.ActiveSection = "Notifications";
        return View(user);
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
    public IActionResult Procedures(ProceduresViewModel model)
    {
        _logger.LogInformation($"{model is null}");
        _logger.LogInformation($"{model?.Procedures is null}");
        //if (!ModelState.IsValid)
        //{
        //    model = new PaymentsViewModel
        //    {
        //        Payments = payments.ToList()
        //    };

        //    _logger.LogInformation("Model state is invalid");
        //    return View("Payments", model);
        //}
        //foreach (var payment in model.Procedures)
        //{
        //    //_logger.LogInformation($"{payment.PaymentStatus.ToString()}");
        //    _procedureDb.Update(payment);
        //}
        _procedureDb.SaveChanges();
        //_logger.LogInformation($"{model.Procedures[0].PaymentStatus.ToString()}");
        return View(model);
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

    [HttpPost]
    public IActionResult SaveProcedures(ProceduresViewModel model)
    {
        var procedures = _procedureDb.GetAll().ToList();
        foreach (var procedure in model.Procedures)
        {
            if(_procedureDb.GetById(procedure.Id) != null)
            {
                _procedureDb.UpdateExisitngModel(procedure);
                continue;
            }

            _procedureDb.Add(procedure);
        }

        _procedureDb.SaveChanges();

        return View("Procedures", model);
    }
}
