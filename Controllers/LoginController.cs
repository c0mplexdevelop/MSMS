using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Repos;
using MSMS.Models;
using MSMS.Models.Dashboard;
using MSMS.Models.Login;
using System.Diagnostics;

namespace MSMS.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private IUserDatabaseRepository _userRepository;

    public LoginController(ILogger<LoginController> logger, IUserDatabaseRepository databaseRepository)
    {
        _logger = logger;
        _userRepository = databaseRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> Login(UserCredential model)
    {
        var queriedUser = _userRepository.GetByCredential(model);
        
        _logger.LogInformation(model.ToString());
        _logger.LogDebug(queriedUser?.ToString());
        _logger.LogInformation(queriedUser is null ? "No queried User" : queriedUser.ToString());
        if (queriedUser is null)
        {
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, queriedUser!.Username),
            new Claim(ClaimTypes.Role, queriedUser!.Role.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow
        };

        _logger.LogInformation(queriedUser.Role.ToString());
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        _userService.SetUser(queriedUser);

        return RedirectToAction("Notifications", "Dashboard", queriedUser);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
