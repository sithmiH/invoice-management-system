using InvoiceManagement.API.DTOs.Invoice;

namespace InvoiceManagement.API.Interfaces.Services;

// Defines business layer operations for managing invoices
public interface IInvoiceService
{
    Task<IEnumerable<InvoiceResponseDto>> GetAllInvoicesAsync();

    Task<IEnumerable<InvoiceResponseDto>> GetInvoicesByUserIdAsync(int userId);

    Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id);

    Task<bool> CreateInvoiceAsync(CreateInvoiceDto request);

    Task<bool> UpdateInvoiceAsync(int id, UpdateInvoiceDto request);

    Task<bool> DeleteInvoiceAsync(int id);
}
