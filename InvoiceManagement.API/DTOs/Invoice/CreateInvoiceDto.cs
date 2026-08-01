namespace InvoiceManagement.API.DTOs.Invoice;

// Represents the data required from a client to create a new invoice
public class CreateInvoiceDto
{
    public string InvoiceNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}
