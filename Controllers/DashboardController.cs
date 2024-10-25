using Microsoft.AspNetCore.Mvc;
using MSMS.Models.Dashboard;

namespace MSMS.Controllers;

public class DashboardController : Controller
{
    public IActionResult Notifications(User user)
    {
        return View(user);
    }
}
