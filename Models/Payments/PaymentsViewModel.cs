namespace MSMS.Models.Payments;

public class PaymentsViewModel
{
    public List<Patient> Patients { get; set; } = null!;
    public List<Payment> Payments { get; set; } = null!;
}
