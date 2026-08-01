namespace InvoiceManagement.API.DTOs.Auth;

// Represents the credentials submitted by a client when log in
public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
