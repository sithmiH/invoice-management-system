using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.DTOs.Invoice;

// Represents the data required from a client to create a new invoice
public class CreateInvoiceDto
{
    [Required(ErrorMessage = "Invoice number is required.")]
    [StringLength(20, ErrorMessage = "Invoice number cannot exceed 20 characters.")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer name is required.")]
    [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, 1000000000, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression("^(Draft|Paid|Cancelled)$",
        ErrorMessage = "Status must be either 'Draft' or 'Paid' or 'Cancelled'.")]
    public string Status { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }
}
