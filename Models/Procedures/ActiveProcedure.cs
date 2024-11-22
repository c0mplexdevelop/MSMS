using Microsoft.EntityFrameworkCore;
using MSMS.Models.Diagnosis;

namespace MSMS.Models.Procedures;


/// <summary>
/// Transfer object for transferring data between subsystems.
/// Used to calculate the total cost of a procedure, and which patient is undergoing the procedure.
/// </summary>
public class ActiveProcedure
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public int ProcedureId { get; set; }
    public Procedure Procedure { get; set; } = null!;
    public DateTime ProcedureServiceDateTime { get; set; }

    public static bool operator ==(ActiveProcedure left, ActiveProcedure right)
    {
        if(ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        if(left.Id == right.Id && left.PatientId == right.PatientId && left.ProcedureId == right.ProcedureId && left.ProcedureServiceDateTime == right.ProcedureServiceDateTime)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(ActiveProcedure left, ActiveProcedure right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is ActiveProcedure activeProcedure)
        {
            return this == activeProcedure;
        }
        return false;
    }
}
