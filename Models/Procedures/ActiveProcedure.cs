using Microsoft.EntityFrameworkCore;

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
}
