using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Web.DTOs.Invoice;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceManagement.Web.Controllers;

//  Handles invoice related views
public class InvoiceController : Controller
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    // Displays the all invoices list
    public async Task<IActionResult> Index()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();

        return View(invoices);
    }

    // Displays the create invoice form
    public async Task<IActionResult> Create()
    {
        var users = await _invoiceService.GetAllUsersAsync();

        ViewBag.Users = new SelectList(users, "Id", "Name");

        return View();
    }

    // Handles submission of the create invoice form
    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceDto request)
    {
        if (!ModelState.IsValid)
        {
            var users = await _invoiceService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name");
            return View(request);
        }

        var success = await _invoiceService.CreateInvoiceAsync(request);

        if (!success)
        {
            ModelState.AddModelError("", "Failed to create invoice.");

            var users = await _invoiceService.GetAllUsersAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name");

            return View(request);
        }

        return RedirectToAction(nameof(Index));
    }

    // Displays an single invoice by Id 
    public async Task<IActionResult> Details(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null)
            return NotFound();

        return View(invoice);
    }

}
