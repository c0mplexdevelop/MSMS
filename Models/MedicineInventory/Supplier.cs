using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.MedicineInventory;

public class Supplier
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string ContactNumber { get; set; } = string.Empty;
}
