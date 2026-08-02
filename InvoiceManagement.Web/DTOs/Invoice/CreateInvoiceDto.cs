using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Web.DTOs.Invoice;

//  Contains the required information for invoice creation
public class CreateInvoiceDto
{
    [Required]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }
}