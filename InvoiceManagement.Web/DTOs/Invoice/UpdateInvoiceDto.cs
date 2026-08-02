using System.ComponentModel.DataAnnotations;

// this used to update an existing invoice
namespace InvoiceManagement.Web.DTOs.Invoice;

public class UpdateInvoiceDto
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}
