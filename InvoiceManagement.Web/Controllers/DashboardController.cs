using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

// Handles the main dashboard view
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
