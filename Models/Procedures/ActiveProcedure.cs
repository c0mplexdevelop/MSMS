using Microsoft.EntityFrameworkCore;

namespace MSMS.Models.Procedures;


/// <summary>
/// Transfer object for transferring data between subsystems.
/// Used to calculate the total cost of a procedure, and which patient is undergoing the procedure.
/// </summary>
[Keyless]
public class ActiveProcedure
{
    public Patient Patient { get; set; } = null!;
    public Procedure Procedure { get; set; } = null!;
}
