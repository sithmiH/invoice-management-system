using InvoiceManagement.API.Interfaces.Services;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Services;

// Logs payment confirmation messages
public class PaymentLoggerService : IPaymentLoggerService
{
    private readonly ILogger<PaymentLoggerService> _logger;

    public PaymentLoggerService(ILogger<PaymentLoggerService> logger)
    {
        _logger = logger;
    }

    public async Task LogPaymentAsync(Invoice invoice)
    {
        // Simulate background processing
        await Task.Delay(1000);

        _logger.LogInformation(
            "Payment confirmed for Invoice {InvoiceNumber} at {Time}",
            invoice.InvoiceNumber,
            DateTime.UtcNow);
    }
}