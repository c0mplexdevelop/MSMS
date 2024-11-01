using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public interface IMedicineDatabaseRepository : IDatabaseRepository<Medicine>
{
    Medicine GetExpiredMedicine(DateOnly expiryDate);
}
