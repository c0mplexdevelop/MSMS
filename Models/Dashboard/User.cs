using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.Dashboard;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 7)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 7)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

}
