namespace MSMS.Models.Payments;

public class Payment
{
    public int Id { get; set; }
    public DateOnly ServiceDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    // Navigation properties (foreign keys)
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}
