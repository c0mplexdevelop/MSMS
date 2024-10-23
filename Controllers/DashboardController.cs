using Microsoft.AspNetCore.Mvc;
using MSMS.Models.Dashboard;

namespace MSMS.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index(User user)
    {
        return View(user);
    }
}
