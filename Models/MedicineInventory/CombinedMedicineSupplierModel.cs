﻿namespace MSMS.Models.MedicineInventory;

public class CombinedMedicineSupplierModel
{
    public IEnumerable<Medicine> Medicines { get; set; } = null!;
    public IEnumerable<Supplier> Suppliers { get; set; } = null!;
}
