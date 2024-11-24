using Microsoft.EntityFrameworkCore;
using MSMS.Models.Login;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;
using MSMS.Models.Procedures;
using MSMS.Auth;
using MSMS.Models.Notification;
using MSMS.Models.Diagnosis;

namespace MSMS.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{

    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Procedure> Procedure { get; set; }
    public DbSet<ActiveProcedure> ActiveProcedures { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>().HasOne(m => m.Supplier).WithMany(supplier => supplier.Medicines).HasForeignKey(m => m.SupplierId);
        modelBuilder.Entity<Notification>().HasOne(n => n.User).WithMany().HasForeignKey(n => n.UserId);

        modelBuilder.Entity<Notification>().Property(entity => entity.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<ActiveProcedure>().HasOne(n => n.Procedure).WithMany().HasForeignKey(n => n.ProcedureId);
        modelBuilder.Entity<ActiveProcedure>().HasOne(n => n.Patient).WithMany().HasForeignKey(n => n.PatientId);

        modelBuilder.Entity<Diagnosis>().Property(entity => entity.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Diagnosis>().HasOne(n => n.Patient).WithMany().HasForeignKey(n => n.PatientId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Diagnosis>().HasOne(n => n.MedicalRecord).WithMany(mr => mr.Diagnoses).HasForeignKey(n => n.MedicalRecordId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MedicalRecord>().Property(entity => entity.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<MedicalRecord>().HasOne(mr => mr.Patient).WithOne(p => p.MedicalRecord);
        modelBuilder.Entity<MedicalRecord>().HasMany(mr => mr.Diagnoses).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);


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
                Age = 40,
                ContactNumber = "1234567890",
                DateOfBirth = new DateTime(1980, 1, 1),
                Gender = Gender.Male,
                
            },
            new Patient
            {
                Id = 2,
                FirstName = "Jane",
                MiddleName = "Doe",
                LastName = "Smith",
                Age = 30,
                ContactNumber = "0987654321",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Female,
                
            });

        modelBuilder.Entity<MedicalRecord>().HasData(
            new MedicalRecord
            {
                Id = 1,
                PatientId = 1,
                Doctor = "Dr. John Doe",

                CurrentMedications = "Paracetamol",

            },
            new MedicalRecord
            {
                Id = 2,
                PatientId = 2,
                Doctor = "Dra. Jane Doe",
                CurrentMedications = "Paracetamol",

            });

        modelBuilder.Entity<Diagnosis>().HasData(           
            new Diagnosis
            {
                Id = 1,
                DiagnosisDetails = "Chickenpox Varicella",
                Doctor = "Dr. John Doe",
                Notes = @"high Fever and Fatigue",
                CreatedAt = new DateTime(2021, 1, 1),
                PatientId = 1,
                MedicalRecordId = 1
            },
            
            new Diagnosis
            {
                Id = 2,
                DiagnosisDetails = "Influenza",
                Doctor = "Dra. Jane Doe",
                Notes = @"Mucus on tne alveoli.",
                CreatedAt = new DateTime(2021, 1, 1),
                PatientId = 2,
                MedicalRecordId = 2

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
                Username = "test123",
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

        modelBuilder.Entity<ActiveProcedure>().HasData(
            new ActiveProcedure
            {
                Id = 1,
                ProcedureId = 1,
                PatientId = 1,
                ProcedureServiceDateTime = DateTime.Now
            },
            new ActiveProcedure
            {
                Id = 2,
                ProcedureId = 1,
                PatientId = 2,
                ProcedureServiceDateTime = DateTime.Now.AddDays(-1)
            });

        base.OnModelCreating(modelBuilder);
    }
}
