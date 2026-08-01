namespace InvoiceManagement.API.DTOs.Invoice;

// Represents the invoice data returned to clients in API responses
public class InvoiceResponseDto
{
    public int InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}