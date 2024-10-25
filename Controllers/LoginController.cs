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

    private IDatabaseRepository<User> _userRepository;

    public LoginController(ILogger<LoginController> logger, IDatabaseRepository<User> databaseRepository)
    {
        _logger = logger;
        _userRepository = databaseRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
        
    [HttpPost]
    public IActionResult Login(UserCredential model)
    {
        var queriedUser = _userRepository.GetByCredential(model);
        _logger.LogInformation(model.ToString());
        _logger.LogDebug(queriedUser?.ToString());
        if (queriedUser is null)
        {
            return View(model);
        }

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
