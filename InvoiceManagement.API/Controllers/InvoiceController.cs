using InvoiceManagement.API.DTOs.Invoice;
using InvoiceManagement.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers;

// Handles CRUD operations for invoices
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    // Retrieves all invoices
    [HttpGet]
    public async Task<IActionResult> GetAllInvoices()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        return Ok(invoices);
    }

    // Retrieves a invoice by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null)
            return NotFound(new { message = "Invoice not found." });

        return Ok(invoice);
    }

    // Creates a new invoice
    [HttpPost]
    public async Task<IActionResult> CreateInvoice(CreateInvoiceDto request)
    {
        var success = await _invoiceService.CreateInvoiceAsync(request);

        if (!success)
            return BadRequest(new { message = "Failed to create invoice." });

        return Ok(new
        {
            message = "Invoice created successfully."
        });
    }

    // Updates an existing invoice
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoiceDto request)
    {
        var success = await _invoiceService.UpdateInvoiceAsync(id, request);

        if (!success)
            return NotFound(new { message = "Invoice not found." });

        return Ok(new
        {
            message = "Invoice updated successfully."
        });
    }

    // Deletes an existing invoice
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var success = await _invoiceService.DeleteInvoiceAsync(id);

        if (!success)
            return NotFound(new { message = "Invoice not found." });

        return Ok(new
        {
            message = "Invoice deleted successfully."
        });
    }
}
