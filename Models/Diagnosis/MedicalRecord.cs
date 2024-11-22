using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSMS.Models.Diagnosis;

public class MedicalRecord
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public string? RecordDetails { get; set; }
    public string? MedicalHistory { get; set; }
    public string? CurrentMedications { get; set; }

    public string? Notes { get; set; }
    public required string Doctor { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    //Navigation Properties
    public virtual Patient? Patient { get; set; }
    
    public virtual ICollection<Diagnosis>? Diagnoses { get; set; }
}

