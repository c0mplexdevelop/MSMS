using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Repos;

public interface IMedicineDatabaseRepository : IDatabaseRepository<Medicine>
{
    IEnumerable<Medicine> GetMedicineByExpiryDate(DateOnly expiryDate);
    IEnumerable<Medicine> GetExpiredMedicine();
}
