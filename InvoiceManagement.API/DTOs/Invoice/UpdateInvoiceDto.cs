using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.DTOs.Invoice;

// Represents the data client can update when updating an existing invoice
public class UpdateInvoiceDto
{
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
}
