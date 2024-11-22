using Microsoft.EntityFrameworkCore;
using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public class MedicineRepository : IMedicineDatabaseRepository
{
    private readonly DatabaseContext context;

    public MedicineRepository(DatabaseContext context)
    {
        this.context = context;
    }

    public void Add(Medicine model)
    {
        throw new NotImplementedException();
    }

    public void Delete(Medicine model)
    {
        throw new NotImplementedException();
    }

    //public MedicineRepository()
    //{
    //var supplier1 = new Supplier
    //{
    //    Id = 1,
    //    Name = "Supplier 1",
    //    Address = "Address 1",
    //    ContactNumber = "1234567890"
    //};
    //var supplier2 = new Supplier
    //{
    //    Id = 2,
    //    Name = "Supplier 2",
    //    Address = "Address 2",
    //    ContactNumber = "0987654321"
    //};

    //if(!context.Suppliers.Any())
    //{
    //    context.Suppliers.AddRange(supplier1, supplier2);
    //    context.SaveChanges();
    //}

    //var medicine1 = new Medicine
    //{
    //    Id = 1,
    //    GenericName = "Paracetamol",
    //    Price = 10.00m,
    //    Quantity = 100,
    //    Supplier = supplier1
    //};

    //var medicine2 = new Medicine
    //{
    //    Id = 2,
    //    GenericName = "Aspirin",
    //    Price = 5.00m,
    //    Quantity = 50,
    //    Supplier = supplier2
    //};


    //if(!context.Medicines.Any())
    //{
    //    context.Medicines.AddRange(medicine1, medicine2);
    //    context.SaveChanges();
    //}

    //if(!context.Medicines.Any() || !context.Suppliers.Any())
    //{
    //    context.SaveChanges();

    //}
    //}
    public IEnumerable<Medicine> GetAll()
    {
        return [.. context.Medicines.Include(m => m.Supplier)];
    }

    public Medicine? GetById(int id)
    {
        return context.Medicines.Include(m => m.Supplier).FirstOrDefault(m => m.Id == id);
    }

    public Medicine? GetByIdWithNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Medicine> GetExpiredMedicine()
    {
        return context.Medicines.Include(m => m.Supplier).Where(m => DateOnly.FromDateTime(DateTime.Now) > m.ExpiryDate);
    }

    public IEnumerable<Medicine> GetMedicineByExpiryDate(DateOnly expiryDate)
    {
        return context.Medicines.Include(m => m.Supplier).Where(m => m.ExpiryDate == expiryDate);

    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExisitngModel(Medicine model)
    {
        context.Medicines.Update(model);
    }
}
