namespace InvoiceManagement.API.DTOs.User;

// this used to return user information from the API
public class UserResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
