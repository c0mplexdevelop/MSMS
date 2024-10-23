using Microsoft.EntityFrameworkCore;
using MSMS.Models.Login;
using MSMS.Models.Dashboard;
using MSMS.Models.MedicineInventory;

namespace MSMS.Data;

public class DatabaseContext : DbContext
{

    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MSMS");
        base.OnConfiguring(optionsBuilder);
    }
}
