namespace InvoiceManagement.API.DTOs.Invoice;

public class CreateInvoiceDto
{
    public string InvoiceNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}
