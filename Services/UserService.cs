
using MSMS.Auth;
using MSMS.Models.Dashboard;
using System.Security.Claims;

namespace MSMS.Services;

public class UserService
{
    private readonly IHttpContextAccessor _httpContext;
    private User? _currentUser;

    public UserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public User GetUser()
    {
        if (_currentUser is not null) return _currentUser;

        var name = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name);
        var role = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role);

        _currentUser = new User
        {
            Username = name?.Value ?? "Anonymous",
            Role = role is null ? UserRole.Guest : Enum.Parse<UserRole>(role.Value)
        };

        return _currentUser;
    }

    public void SetUser(User user)
    {
        _currentUser = user;
    }
}
