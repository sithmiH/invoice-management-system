using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.DTOs.Auth;

// Represents the data submitted by a client when registering
public class RegisterRequestDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
