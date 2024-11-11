using MSMS.Auth;
using System.ComponentModel.DataAnnotations;

namespace MSMS.Models.Dashboard;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Username: {Username}, Password: {Password}, Role: {Role}";
    }

}
