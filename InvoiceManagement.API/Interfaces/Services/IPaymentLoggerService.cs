using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Interfaces.Services;

// Defines the operations for logging payment confirmation messages
public interface IPaymentLoggerService
{
    Task LogPaymentAsync(Invoice invoice);
}
