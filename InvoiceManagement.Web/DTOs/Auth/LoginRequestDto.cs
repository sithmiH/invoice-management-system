namespace InvoiceManagement.Web.DTOs.Auth;

// Represents the login data submitted from the login form
public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
