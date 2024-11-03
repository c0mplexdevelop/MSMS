using Microsoft.EntityFrameworkCore;
using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public class MedicineRepository : IMedicineDatabaseRepository
{
    public MedicineRepository()
    {
        using var context = new DatabaseContext();

        var supplier1 = new Supplier
        {
            Id = 1,
            Name = "Supplier 1",
            Address = "Address 1",
            ContactNumber = "1234567890"
        };
        var supplier2 = new Supplier
        {
            Id = 2,
            Name = "Supplier 2",
            Address = "Address 2",
            ContactNumber = "0987654321"
        };

        if(!context.Suppliers.Any())
        {
            context.Suppliers.AddRange(supplier1, supplier2);
            context.SaveChanges();
        }

        var medicine1 = new Medicine
        {
            Id = 1,
            GenericName = "Paracetamol",
            Price = 10.00m,
            Quantity = 100,
            Supplier = supplier1
        };

        var medicine2 = new Medicine
        {
            Id = 2,
            GenericName = "Aspirin",
            Price = 5.00m,
            Quantity = 50,
            Supplier = supplier2
        };

        
        if(!context.Medicines.Any())
        {
            context.Medicines.AddRange(medicine1, medicine2);
            context.SaveChanges();
        }

        //if(!context.Medicines.Any() || !context.Suppliers.Any())
        //{
        //    context.SaveChanges();

        //}

    public Medicine? GetById(int id)
    {
        return context.Medicines.Include(m => m.Supplier).FirstOrDefault(m => m.Id == id);
    }

    }
    public IEnumerable<Medicine> GetAll()
    {
        using var context = new DatabaseContext();
        return [.. context.Medicines.Include(m => m.Supplier)];
    }

    public Medicine GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Medicine GetExpiredMedicine(DateOnly expiryDate)
    {
        throw new NotImplementedException();
    }
}
