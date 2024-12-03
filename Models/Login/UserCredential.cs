using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.Login;

public class UserCredential
{
    [Required]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "Invalid Credentials")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(int.MaxValue, MinimumLength = 7, ErrorMessage = "Invalid Credentials")]
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Username} {Password}";
    }
}
