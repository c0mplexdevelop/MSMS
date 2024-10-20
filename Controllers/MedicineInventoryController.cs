using Microsoft.AspNetCore.Mvc;

using MSMS.Models.MedicineInventory;

namespace MSMS.Controllers;

public class MedicineInventoryController : Controller
{
    public ILogger<MedicineInventoryController> Logger { get; set; } = null!;

    public IActionResult MedicineView()
    {
        var testModel = new CombinedMedicineSupplierModel
        {
            Medicines =
            [
                new() {
                    Id = 1,
                    GenericName = "Paracetamol",
                    Price = 10.00m,
                    ActualQuantity = 100
                },
                new() {
                    Id = 2,
                    GenericName = "Aspirin",
                    Price = 5.00m,
                    ActualQuantity = 50
                }
            ],
            Suppliers =
            [
                new() {
                    Id = 1,
                    Name = "Supplier 1",
                    Address = "Address 1",
                    ContactNumber = "1234567890"
                },
                new() {
                    Id = 2,
                    Name = "Supplier 2",
                    Address = "Address 2",
                    ContactNumber = "0987654321"
                }
            ]
        };

        return View(testModel);
    }
}
