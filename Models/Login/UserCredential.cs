using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.Login;

public class UserCredential
{
    [Required]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "Username must be between 7 and 20 characters long")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(int.MaxValue, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters long")]
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Username} {Password}";
    }
}
