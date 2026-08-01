using InvoiceManagement.API.DTOs.Invoice;
using InvoiceManagement.API.Interfaces.Repositories;
using InvoiceManagement.API.Interfaces.Services;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Services;

// Handles business logic for invoice management
public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentLoggerService _paymentLoggerService;

    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        IPaymentLoggerService paymentLoggerService)
    {
        _invoiceRepository = invoiceRepository;
        _paymentLoggerService = paymentLoggerService;
    }

    // Retrieves all invoices and maps them to response DTOs
    public async Task<IEnumerable<InvoiceResponseDto>> GetAllInvoicesAsync()
    {
        var invoices = await _invoiceRepository.GetAllInvoicesAsync();

        return invoices.Select(invoice => new InvoiceResponseDto
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceNumber = invoice.InvoiceNumber,
            CustomerName = invoice.CustomerName,
            Amount = invoice.Amount,
            Status = invoice.Status,
            CreatedDate = invoice.CreatedDate
        });
    }

    // Retrieves an invoice by ID
    public async Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id)
    {
        var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

        if (invoice == null)
        {
            return null;
        }

        return new InvoiceResponseDto
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceNumber = invoice.InvoiceNumber,
            CustomerName = invoice.CustomerName,
            Amount = invoice.Amount,
            Status = invoice.Status,
            CreatedDate = invoice.CreatedDate
        };
    }

    // Creates a new invoice
    public async Task<bool> CreateInvoiceAsync(CreateInvoiceDto request)
    {
        var invoice = new Invoice
        {
            InvoiceNumber = request.InvoiceNumber,
            CustomerName = request.CustomerName,
            Amount = request.Amount,
            Status = request.Status,
            UserId = request.UserId
        };

        var invoiceId = await _invoiceRepository.CreateInvoiceAsync(invoice);

        return invoiceId > 0;
    }

    // Updates an existing invoice
    public async Task<bool> UpdateInvoiceAsync(int id, UpdateInvoiceDto request)
    {
        var existingInvoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

        if (existingInvoice == null)
        {
            return false;
        }

        existingInvoice.CustomerName = request.CustomerName;
        existingInvoice.Amount = request.Amount;
        existingInvoice.Status = request.Status;

        var updated = await _invoiceRepository.UpdateInvoiceAsync(existingInvoice);

        if (updated &&
            existingInvoice.Status.Equals("Paid", StringComparison.OrdinalIgnoreCase))
        {
            _ = Task.Run(async () =>
            {
                await _paymentLoggerService.LogPaymentAsync(existingInvoice);
            });
        }

        return updated;
    }

    // Deletes an existing invoice
    public async Task<bool> DeleteInvoiceAsync(int id)
    {
        return await _invoiceRepository.DeleteInvoiceAsync(id);
    }

    // Retrieves all invoices belonging to a specific user
    public async Task<IEnumerable<InvoiceResponseDto>> GetInvoicesByUserIdAsync(int userId)
    {
        var invoices = await _invoiceRepository.GetInvoicesByUserIdAsync(userId);

        return invoices.Select(invoice => new InvoiceResponseDto
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceNumber = invoice.InvoiceNumber,
            CustomerName = invoice.CustomerName,
            Amount = invoice.Amount,
            Status = invoice.Status,
            CreatedDate = invoice.CreatedDate
        });
    }
}
