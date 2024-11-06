using System.ComponentModel.DataAnnotations.Schema;

namespace MSMS.Models.Procedures;

public class Procedure
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ProcedureName { get; set; } = string.Empty;
    public decimal ProcedurePrice { get; set; }
    public string ProcedureDescription { get; set; } = string.Empty;
    public string? ProcedureNotes { get; set; } = string.Empty;
    //public DateOnly ServiceDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    //public PaymentType PaymentType { get; set; }
    //public PaymentStatus PaymentStatus { get; set; }

    // Navigation properties (foreign keys)
    //public int PatientId { get; set; }
    //public Patient Patient { get; set; } = null!;
}
