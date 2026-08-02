namespace InvoiceManagement.Web.DTOs.Auth;

// Represents the response returned by the API after a successful login
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
