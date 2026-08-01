namespace InvoiceManagement.API.DTOs.Invoice;

public class UpdateInvoiceDto
{
    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}
