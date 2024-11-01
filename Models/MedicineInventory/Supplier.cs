
namespace MSMS.Models.MedicineInventory;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;

    public ICollection<Medicine> Medicines { get; set; } = null!;
}
