using System.ComponentModel.DataAnnotations;
namespace MSMS.Models.Login;
public class Account
{
    [Key]
    public int EmployeeNumber { get; set; }
    public string? EmployeeID { get; set; }
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your username or email address.")]
    public string Username { get; set; } = string.Empty;

    public string? ContactNo { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Specialization { get; set; }

    public string? Schedule { get; set; }
    public string? Role { get; set; }

    public string? SubSystem { get; set; }
    [Required(ErrorMessage = "Please enter your username or email address.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;


    // Optional property for a generic error message, e.g., for invalid credentials
    public string? ErrorMessage { get; set; }
    
    public string? ConfirmPassword { get; set; }

    public override string ToString()
    {

        return $"{EmployeeNumber} - {Name}";
    }
}
