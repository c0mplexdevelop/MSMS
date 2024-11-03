using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public class SupplierRepository : IDatabaseRepository<Supplier> 
{
    private readonly DatabaseContext context;

    public SupplierRepository(DatabaseContext context)
    {
        this.context = context;
    }
    public Supplier? GetById(int id)
    {
        return context.Suppliers.Find(id);
    }

    public IEnumerable<Supplier> GetAll()
    {
        return [.. context.Suppliers];
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Update(Supplier model)
    {
        context.Suppliers.Update(model);
    }
}
