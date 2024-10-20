using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.MedicineInventory;

public class Medicine
{
    public int Id { get; set; }
    [Required]
    public string BrandName { get; set; } = string.Empty;
    [Required]
    public string GenericName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public int ActualQuantity { get; set; }
    public int RecommendedQuantity { get; set; }
    public int ReorderLevel { get; set; } // This field will be used to determine when to reorder the medicine
    public decimal Price { get; set; }
    public Supplier Supplier { get; set; } = null!;
}
