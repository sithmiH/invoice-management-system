namespace InvoiceManagement.Web.DTOs.Auth;

// Represents the registration data submitted from the register form
public class RegisterRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

}
