using MSMS.Models.Payments;

namespace MSMS.Data.Repos;

public interface IPaymentDatabaseRepository : IDatabaseRepository<Payment>
{
    IEnumerable<Payment?> GetAllPaymentByPaymentStatus(PaymentStatus status);
    IEnumerable<Payment?> GetAllPaymentsByServiceDate(DateOnly serviceDate);
    IEnumerable<Payment?> GetAllPaymentsByPaymentType(PaymentType type);
    IEnumerable<Payment?> GetAllPaymentsOfPatient(Patient patient);

}
