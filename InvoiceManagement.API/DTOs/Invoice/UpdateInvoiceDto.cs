namespace InvoiceManagement.API.DTOs.Invoice;

// Represents the data client can update when updating an existing invoice
public class UpdateInvoiceDto
{
    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}
