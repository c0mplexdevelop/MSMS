using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public int EmployeeNumber { get; set; }
    public string? EmployeeID { get; set; }
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your username or email address.")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string Username { get; set; } = string.Empty;

    public string? ContactNo { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Specialization { get; set; }

    public string? Schedule { get; set; }
    public string? Role { get; set; }

    public string? SubSystem { get; set; }
    [Required(ErrorMessage = "Please enter your password.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
    public string Password { get; set; } = string.Empty;

    // Optional property for a generic error message, e.g., for invalid credentials
    public string? ErrorMessage { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
    public string? ConfirmPassword { get; set; }
}
