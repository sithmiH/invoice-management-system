using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Interfaces.Repositories;

// This interface provides methods for retrieve, create,update, and delete invoice records
public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();

    Task<IEnumerable<Invoice>> GetInvoicesByUserIdAsync(int userId);

    Task<Invoice?> GetInvoiceByIdAsync(int id);

    Task<int> CreateInvoiceAsync(Invoice invoice);

    Task<bool> UpdateInvoiceAsync(Invoice invoice);

    Task<bool> DeleteInvoiceAsync(int id);
}
