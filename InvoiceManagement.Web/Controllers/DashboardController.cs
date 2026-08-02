using InvoiceManagement.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

// Handles the main dashboard view

[SessionAuthorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
