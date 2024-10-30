using Microsoft.AspNetCore.Mvc;
using MSMS.Models.Dashboard;

namespace MSMS.Controllers;

public class DashboardController : Controller
{
    private ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }

    public IActionResult Notifications(User user)
    {
        ViewBag.ActiveSection = "Notifications";
        return View(user);
    }

    public IActionResult Inventory(User user)
    {
        ViewBag.ActiveSection = "Inventory";
        return View(user);
    }

    public IActionResult Diagnosis(User user)
    {
        ViewBag.ActiveSection = "Diagnosis";
        return View(user);
    }

    public IActionResult Reports(User user)
    {
        ViewBag.ActiveSection = "Reports";
        return View(user);
    }

    public IActionResult Payments(User user)
    {
        ViewBag.ActiveSection = "Payments";
        return View(user);
    }
}
