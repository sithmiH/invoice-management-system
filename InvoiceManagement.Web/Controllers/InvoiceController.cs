using InvoiceManagement.Web.DTOs.Invoice;
using InvoiceManagement.Web.Filters;
using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceManagement.Web.Controllers;

[SessionAuthorize]
//  Handles invoice related views
public class InvoiceController : Controller
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    // Displays the all invoices list
    public async Task<IActionResult> Index(int? invoiceId)
    {
        if (invoiceId.HasValue)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId.Value);

            if (invoice == null)
            {
                TempData["Error"] = "Invoice not found.";
                return View(new List<InvoiceResponseDto>());
            }

            return View(new List<InvoiceResponseDto> { invoice });
        }

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

            TempData["Error"] = "Failed to create invoice.";
            return View(request);
        }

        TempData["Success"] = "Invoice created successfully.";
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

    // Displays the invoice edit form
    public async Task<IActionResult> Edit(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null)
            return NotFound();

        var model = new UpdateInvoiceDto
        {
            CustomerName = invoice.CustomerName,
            Amount = invoice.Amount,
            Status = invoice.Status
        };

        ViewBag.InvoiceId = id;

        return View(model);
    }

    // Handles submission of invoice update form
    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateInvoiceDto request)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.InvoiceId = id;
            return View(request);
        }

        var success = await _invoiceService.UpdateInvoiceAsync(id, request);

        if (!success)
        {
            ModelState.AddModelError("", "Failed to update invoice.");
            ViewBag.InvoiceId = id;
            TempData["Error"] = "Failed to update invoice.";
            return View(request);
        }

        TempData["Success"] = "Invoice updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Displays the invoice delete confirmation page
    public async Task<IActionResult> Delete(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null)
            return NotFound();

        return View(invoice);
    }

    // Handles invoice delete after confirmation
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _invoiceService.DeleteInvoiceAsync(id);

        if (!success)
            return BadRequest();

        TempData["Success"] = "Invoice deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

}
