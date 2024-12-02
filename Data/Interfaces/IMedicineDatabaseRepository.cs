using MSMS.Models.MedicineInventory;

namespace MSMS.Data.Interfaces;

public interface IMedicineDatabaseRepository : IDatabaseRepository<Medicine>
{
    IEnumerable<Medicine> GetMedicineByExpiryDate(DateOnly expiryDate);
    IEnumerable<Medicine> GetExpiredMedicine();
}
