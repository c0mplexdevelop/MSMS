using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public class SupplierRepository() : IDatabaseRepository<Supplier> 
{

    public Supplier GetById(int id)
    {
        using var context = new DatabaseContext();
        return context.Suppliers.Find(id);
    }

    public IEnumerable<Supplier> GetAll()
    {
        using var context = new DatabaseContext();
        return [.. context.Suppliers];
    }
}
