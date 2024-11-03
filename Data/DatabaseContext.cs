using Microsoft.EntityFrameworkCore;
using MSMS.Models.Login;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Payments;

namespace MSMS.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{

    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>().HasOne(m => m.Supplier).WithMany(supplier => supplier.Medicines).HasForeignKey(m => m.SupplierId);

        modelBuilder.Entity<Supplier>().HasData(
            new Supplier
            {
                Id = 1,
                Name = "Supplier 1",
                Address = "Address 1",
                ContactNumber = "1234567890"
            },
            new Supplier
            {
                Id = 2,
                Name = "Supplier 2",
                Address = "Address 2",
                ContactNumber = "0987654321"
            }
        );

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine
            {
                Id = 1,
                GenericName = "Paracetamol",
                Price = 10.00m,
                Quantity = 100,
                SupplierId = 1
            },

            new Medicine
            {
                Id = 2,
                GenericName = "Aspirin",
                Price = 5.00m,
                Quantity = 50,
                SupplierId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
