using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSMS.Models.Diagnosis;


public class Diagnosis
{
    public int Id { get; set; }

    [ForeignKey("Patient")]
    public int PatientId { get; set; }

    public string DiagnosisDetails { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public string Doctor { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Patient Patient { get; set; } = null!;
}
