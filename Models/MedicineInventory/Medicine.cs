
namespace MSMS.Models.MedicineInventory;

public class Medicine
{
    public int Id { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string GenericName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int Dosage { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public DateOnly SuppliedDate { get; set; }
    public decimal Price { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    /*
     * If Quantity is less than ReorderLevel, then reorder the medicine
     */
    public int ReorderLevel { get; set; } // This field will be used to determine when to reorder the medicine

    public override string ToString()
    {
        return $"Id: {Id}, BrandName: {BrandName}, GenericName: {GenericName}, Quantity: {Quantity}, Dosage: {Dosage}, ExpiryDate: {ExpiryDate}, SuppliedDate: {SuppliedDate}, Price: {Price}, Supplier: {Supplier}, ReorderLevel: {ReorderLevel}";
    }

}
