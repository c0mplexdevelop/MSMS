using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public class SupplierRepository : IDatabaseRepository<Supplier> 
{
    private readonly DatabaseContext context;

    public SupplierRepository(DatabaseContext context)
    {
        this.context = context;
    }
        return context.Suppliers.Find(id);
    }

    public IEnumerable<Supplier> GetAll()
    {
        return [.. context.Suppliers];
    }
}
