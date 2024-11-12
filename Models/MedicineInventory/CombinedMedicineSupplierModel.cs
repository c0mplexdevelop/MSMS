using MSMS.Models.Dashboard;

namespace MSMS.Models.MedicineInventory;

public class CombinedMedicineSupplierModel
{
    public IEnumerable<Medicine> Medicines { get; set; } = null!;
    public IEnumerable<Supplier> Suppliers { get; set; } = null!;
    public User User { get; set; } = null!;
    public string SearchString { get; set; } = string.Empty;
}
