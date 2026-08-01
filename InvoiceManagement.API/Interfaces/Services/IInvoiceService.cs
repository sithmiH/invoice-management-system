using InvoiceManagement.API.DTOs.Invoice;

namespace InvoiceManagement.API.Interfaces.Services;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceResponseDto>> GetAllInvoicesAsync();

    Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id);

    Task<bool> CreateInvoiceAsync(CreateInvoiceDto request);

    Task<bool> UpdateInvoiceAsync(int id, UpdateInvoiceDto request);

    Task<bool> DeleteInvoiceAsync(int id);
}
