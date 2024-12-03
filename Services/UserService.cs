
using Microsoft.EntityFrameworkCore;
using MSMS.Auth;
using MSMS.Data;
using MSMS.Models.Dashboard;
using MSMS.Models.Login;
using System.Security.Claims;
using System.Text.Json;

namespace MSMS.Services;

public class UserService
{
    private readonly IHttpContextAccessor _httpContext;
    private Account? _currentUser;
    private ILogger<UserService> _logger;

    public UserService(IHttpContextAccessor httpContext, ILogger<UserService> logger)
    {
        _httpContext = httpContext;
        _logger = logger;
    }

    public Account GetUser()
    {
        _logger.LogInformation("Getting User..");
        if (_currentUser is not null) return _currentUser;

        var userJson = _httpContext.HttpContext.Session.GetString("User");
        if(userJson is not null)
        {
            _currentUser = JsonSerializer.Deserialize<Account>(userJson)!;
            return _currentUser;
        }

        var name = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name);
        var role = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role);

        _currentUser = new Account
        {
            Username = name?.Value ?? "Anonymous",
            Role = role is null ? UserRole.Guest.ToString() : Enum.Parse<UserRole>(role.Value).ToString()
        };

        _logger.LogInformation($"User: {_currentUser.Username}, {_currentUser.Role}");


        return _currentUser;
        //if (_currentUser is not null) return _currentUser;

        //var userJson = _httpContext.HttpContext.Session.GetString("User");
        //if (userJson is not null)
        //{
        //    _currentUser = JsonSerializer.Deserialize<User>(userJson); // Ensure the deserialized user is tracked by the context
        //    var context = _httpContext.HttpContext.RequestServices.GetService<DatabaseContext>(); 
        //    var trackedUser = context.Users.Find(_currentUser.Id); 
        //    if (trackedUser != null)
        //    { 
        //        context.Entry(trackedUser).CurrentValues.SetValues(_currentUser); 
        //        _currentUser = trackedUser; 
        //    } else 
        //    { 
        //        context.Users.Attach(_currentUser); 
        //        context.Entry(_currentUser).State = EntityState.Modified; 
        //    }
        //}

        //var name = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name);
        //var role = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role);

        //_currentUser = new User
        //{
        //    Username = name?.Value ?? "Anonymous",
        //    Role = role is null ? UserRole.Guest : Enum.Parse<UserRole>(role.Value)
        //};

        //return _currentUser;
    }

    public void SetUser(Account? user)
    {
        _currentUser = user;
    }
}
