using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceManagement.Web.DTOs.User;

// this used to return user information in API responses
public class UserResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
