namespace MSMS.Models.Diagnosis;

public class PatientViewModel
{
    public IList<Patient> Patients { get; set; } = [
            new() {
                Id = 1,
                FirstName = "John",
                MiddleName = "Doe",
                LastName = "Smith",
                DateOfBirth = new DateTime(1990, 1, 1),
                Age = 31,
            }
        ];
}
