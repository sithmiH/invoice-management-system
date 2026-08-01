using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
