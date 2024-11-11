using Microsoft.EntityFrameworkCore;
using MSMS.Models.Login;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Procedures;
using MSMS.Auth;
using MSMS.Models.Notification;

namespace MSMS.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{

    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Procedure> Procedure { get; set; }
    public DbSet<ActiveProcedure> PatientProcedures { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>().HasOne(m => m.Supplier).WithMany(supplier => supplier.Medicines).HasForeignKey(m => m.SupplierId);
        modelBuilder.Entity<Notification>().HasOne(n => n.User).WithMany().HasForeignKey(n => n.UserId);

        modelBuilder.Entity<Notification>().Property(entity => entity.Id).ValueGeneratedOnAdd();

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
                SupplierId = 1,
                ReorderLevel = 50
            },

            new Medicine
            {
                Id = 2,
                GenericName = "Aspirin",
                Price = 5.00m,
                Quantity = 50,
                SupplierId = 2,
                ReorderLevel = 75
            });

        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "John",
                MiddleName = "Doe",
                LastName = "Smith",
                ContactNumber = "1234567890"
            },
            new Patient
            {
                Id = 2,
                FirstName = "Jane",
                MiddleName = "Doe",
                LastName = "Smith",
                ContactNumber = "0987654321"
            });

        modelBuilder.Entity<Procedure>().HasData(
            new Procedure
            {
                Id = 1,
                ProcedureName = "Blood Test",
                ProcedureDescription = "Blood Test Duh",
                ProcedurePrice = 100.00m,
                ProcedureNotes = "Disposable Syringe"
            },
            new Procedure
            {
                Id = 2,
                ProcedureName = "Urinalysis",
                ProcedureDescription = "Analyze patient urine for bacteria or unbalanced electrolyte level.",
                ProcedurePrice = 100.00m,
                ProcedureNotes = "Make Patient drink water till they excrete."
            });


        modelBuilder.Entity<User>().HasData(
            new()
            {
                Id = 1,
                Name = "John Doe",
                Username = "c0mplex",
                Password = "test123",
                Role = UserRole.Admin
            },
            new() {
                Id = 2,
                Name = "Jane Doe",
                Username = "test",
                Password = "test123",
                Role = UserRole.Guest
            });

        modelBuilder.Entity<Notification>().HasData(
            new Notification
            {
                Id = 1,
                UserId = 1,
                Message = "Hello, World!",
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)

            });

        base.OnModelCreating(modelBuilder);
    }
}
