using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSMS.Models.Procedures;

public class Procedure
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ProcedureName { get; set; } = string.Empty;
    [Precision(38,2)] // 65 digits in total, 2 after the decimal point
    public decimal ProcedurePrice { get; set; }
    public string ProcedureDescription { get; set; } = string.Empty;
    public string? ProcedureNotes { get; set; } = string.Empty;
    //public DateOnly ServiceDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    //public PaymentType PaymentType { get; set; }
    //public PaymentStatus PaymentStatus { get; set; }

    // Navigation properties (foreign keys)
    //public int PatientId { get; set; }
    //public Patient Patient { get; set; } = null!;

    public static bool operator ==(Procedure left, Procedure right)
    {
        if(ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;

        return left.Id == right.Id && 
            left.ProcedureName == right.ProcedureName && 
            left.ProcedurePrice == right.ProcedurePrice && 
            left.ProcedureDescription == right.ProcedureDescription &&
            left.ProcedureNotes == right.ProcedureNotes;
            
    }

    public static bool operator !=(Procedure left, Procedure right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is Procedure procedure)
        {
            return this == procedure;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
