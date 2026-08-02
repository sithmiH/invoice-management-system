using InvoiceManagement.Web.DTOs.Invoice;
using InvoiceManagement.Web.DTOs.User;

namespace InvoiceManagement.Web.Services;

// Defines the contract for calling the backend Invoice and User endpoints
public interface IInvoiceService
{
    Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();

    Task<bool> CreateInvoiceAsync(CreateInvoiceDto request);

    Task<bool> UpdateInvoiceAsync(int id, UpdateInvoiceDto request);

    Task<bool> DeleteInvoiceAsync(int id);

    Task<List<UserResponseDto>> GetAllUsersAsync();

    Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id);
}
